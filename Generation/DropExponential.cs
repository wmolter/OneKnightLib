using UnityEngine;
using System.Collections.Generic;

namespace OneKnight.Generation {
    [System.Serializable]
    public class DropExponential : ItemEmitter {


        [System.Serializable]
        public class Range {
            public string id;
            public float min = 0;
            public float max = 3;
            public float power = 10;
        }

        [SerializeField]
        private Range[] exponents;
        public float weight = 1;

        public float Weight { get { return weight; } }


        public DropExponential() {
            weight = 0;
            exponents = new Range[0];
        }

        public DropExponential(List<InventoryItem> items) {
            exponents = new Range[items.Count];
            for(int i = 0; i < items.Count; i++) {
                exponents[i] = new Range() {
                    id=items[i].ID,
                    min=1,
                    max=1,
                    power=items[i].count,
                };
            }
        }

        public DropExponential(Range[] exponents, float weight){
            this.weight = weight;
            this.exponents = exponents;
        }

        public IEnumerable<InventoryItem> Generate() {
            for(int i = 0; i < exponents.Length; i++) {
                int count = (int)UniformGeneration.RandomExponential(exponents[i].min, exponents[i].max, exponents[i].power);
                int stackLimit = ItemInfo.StackLimit(exponents[i].id);
                while(count >= stackLimit) {
                    yield return new InventoryItem(exponents[i].id, stackLimit);
                    count -= stackLimit;
                }
                if(count >= 0)
                    yield return new InventoryItem(exponents[i].id, count);
            }
        }

        public IEnumerable<InventoryItem> Generate(int times) {
            for(int i = 0; i < times; i++) {
                foreach(InventoryItem item in Generate())
                    yield return item;
            }
        }
    }
}