using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace OneKnight.UI {
    public class InventoryManagerLight : MonoBehaviour {

        Inventory inventory;
        public Inventory Inventory { get { return inventory; } }
        IEnumerable<InventoryItem> itemList;
        public ReusableElementManager elementManager;

        public bool ValidateOnChildChange;
        public List<string> categories;

        // Use this for initialization

        public void SetInventory(Inventory toDisplay) {
            if(toDisplay == inventory)
                return;
            if(inventory != null)
                inventory.OnChange -= OnInventoryChanged;
            itemList = null;
            inventory = toDisplay;
            if(inventory != null)
                inventory.OnChange += OnInventoryChanged;
            Validate();
        }

        public void SetItemList(IEnumerable<InventoryItem> list) {
            if(list is Inventory inv) {
                SetInventory(inv);
                return;
            }
            if(list == itemList)
                return;
            if(inventory != null) {
                inventory.OnChange -= OnInventoryChanged;
                inventory = null;
            }
            itemList = list;
            Validate();
        }

        public void AddListener(UnityAction<ItemSlot, PointerEventData> listener) {
            foreach(GameObject el in elementManager) {
                el.GetComponent<ItemDisplayLight>().OnClick += listener;
            }
        }

        public void RemoveListener(UnityAction<ItemSlot, PointerEventData> listener) {
            foreach(GameObject el in elementManager) {
                el.GetComponent<ItemDisplayLight>().OnClick -= listener;
            }
        }

        private void OnEnable() {
            Validate();
        }


        public void Validate() {
            if(Inventory != null) {
                int index = 0;
                foreach(ItemSlot slotInv in Inventory) {
                    if(Filter(slotInv)) {
                        InitSlot(slotInv, index);
                        index++;
                    }
                }
                elementManager.DisableFrom(index);
            } else if(itemList != null) {
                int index = 0;
                foreach(InventoryItem item in itemList) {
                    ItemSlot slot = new ItemSlot(item, null);
                    if(Filter(slot)) {
                        InitSlot(slot, index);
                        index++;
                    }
                }
                elementManager.DisableFrom(index);
            } else {
                elementManager.DisableFrom(0);
            }
        }

        protected bool Filter(ItemSlot slot) {
            if(categories == null ||categories.Count == 0)
                return true;
            if(slot == null || slot.Item == null)
                return false;
            bool result = categories.Contains(slot.Item.Category);
            return result;
        }

        protected virtual void InitSlot(ItemSlot slotInv, int index) {
            ItemDisplayLight disp = elementManager.ObjectAt(index).GetComponent<ItemDisplayLight>();
            disp.Init(slotInv);
            disp.gameObject.SetActive(true);
        }

        private void OnInventoryChanged(Inventory i, ItemSlot.EventInfo info) {
            Validate();
        }
        
        /*
        public void TryTakeCursorItem() {
            if(!SingleItemSlot.TempSlot.Empty) {
                if(acceptsItems) {
                    TakeCursorItem();
                }
                //Debug.Log("Moved item: " + ItemOnCursor.item + " to inventory: " + this);
            }

        }

        public void TakeCursorItem() {
            if(AddItem(SingleItemSlot.TempSlot.Item)) {
                SingleItemSlot.TempSlot.RemoveItem();
            }

        }*/
    }
}