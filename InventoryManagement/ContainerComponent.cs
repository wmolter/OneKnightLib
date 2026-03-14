using UnityEngine;
using System.Collections.Generic;
using OneKnight.UI;
using UnityEngine.Events;


namespace OneKnight.InventoryManagement {
    public class ContainerComponent : MonoBehaviour {
        Inventory inventory;

        public Inventory Inventory {
            get {
                return inventory;
            }
            set {
                inventory = value;
                onSetInventory?.Invoke(inventory);
            }
        }
        public UnityEvent<Inventory> onSetInventory;

        public float storageCapacity = 100;

        private void Start() {
            Inventory = new VolumeInventory(storageCapacity);
        }
    }
}