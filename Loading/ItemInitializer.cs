using System.Collections;
using UnityEngine;

namespace OneKnight.Loading {
    public interface ItemInitializer {
        InventoryItem.Data InitializeWithCategory(string category);
    }
}