using UnityEngine;
using System.Collections;

namespace OneKnight.Generation {
    [System.Serializable]
    public class WeightedObject<T> : Weighted where T: Object{

        public float weight;
        public T obj;
        public float Weight { get { return weight; } }
    }
}