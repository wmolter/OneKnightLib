using System.Collections;
using UnityEngine;

namespace OneKnight._2D {
    [RequireComponent(typeof(Renderer))]
    // Adjusts values of sorting order and z according to y position and sorting layer, respectively, so
    // that objects render and raycast in sorting layer, then y position order.
    public class SortSprite : MonoBehaviour {

        public float sortingOffset;
        [Range(0, .99f)]
        public float colliderOffset;
        // Use this for initialization
        void Start() {
            UpdateSortingOrder();
        }

        void UpdateSortingOrder() {
            GetComponent<Renderer>().sortingOrder = (int)(-32*(transform.position.y+sortingOffset));
            int layerID = GetComponent<Renderer>().sortingLayerID;
            int sortLayer = SortingLayer.GetLayerValueFromID(layerID);
            transform.position = new Vector3(transform.position.x, transform.position.y, -sortLayer-colliderOffset);
        }

        void LateUpdate() {
            UpdateSortingOrder();
        }
    }
}