using UnityEngine;
using System.Collections.Generic;
using OneKnight.UI;
using UnityEngine.Events;


namespace OneKnight.InventoryManagement {
    public class ContainerComponent : MonoBehaviour {
        Inventory inventory;
        public string gainItemKey = "gainedItem";
        public string loseItemKey = "lostItem";
        public string noRoomKey = "noRoom";

        public UnityEvent<InventoryItem> onCountChange;

        private Inventory Inventory {
            get {
                return inventory;
            }
            set {
                if(inventory != null)
                    inventory.OnChange -= OnInventoryChange;
                inventory = value;
                inventory.OnChange += OnInventoryChange;
            }
        }

        public float storageCapacity = 100;

        private void Start() {
            if(storageCapacity > 0)
                Inventory = new VolumeInventory(storageCapacity);
        }

        public virtual Inventory GetInventory() {
            return inventory;
        }

        public virtual InventoryItem Insert(InventoryItem item) {
            if(Inventory == null)
                return item;
            InventoryItem remaining = Inventory.AddStackItem(item);
            if(remaining != null && remaining.count > 0) {
                Notifications.CreateError(transform.position, Strings.Format(noRoomKey, remaining.ToString()));
            }
            return remaining;
        }

        public void NotifyChange(Vector3 pos, ItemSlot.EventInfo info) {
            int beforeCount = 0;
            int afterCount = 0;
            string beforeID = "";
            string afterID = "";
            if(info.before != null) {
                beforeCount = info.before.count;
                beforeID = info.before.ID;
            }
            if(info.after != null) {
                afterCount = info.after.count;
                afterID = info.after.ID;
            }
            InventoryItem added = null;
            InventoryItem lost = null;
            if(beforeID == afterID) {
                int change = afterCount - beforeCount;
                if(change > 0)
                    added = new InventoryItem(afterID, change);
                else if(change < 0)
                    lost = new InventoryItem(beforeID, -change);
            } else {
                if(afterCount > 0) {
                    added = info.after;
                }
                if(beforeCount > 0) {
                    lost = info.before;
                }
            }
            if(added != null) {
                Notifications.CreateNotification(pos, Strings.Format(gainItemKey, added.ToString()));
                onCountChange.Invoke(added);
            }
            if(lost != null) {
                Notifications.CreateNegative(pos, Strings.Format(gainItemKey, lost.ToString()));
                onCountChange.Invoke(new InventoryItem(lost.ID, -lost.count));
            }
        }

        private void OnInventoryChange(Inventory inv, ItemSlot.EventInfo info) {
            NotifyChange(transform.position, info);
        }
    }
}