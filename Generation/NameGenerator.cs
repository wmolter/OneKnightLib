using System.Collections.Generic;
using UnityEngine;

namespace OneKnight.Generation {
    [CreateAssetMenu(menuName = "Generation/Name Generator/Name Generator")]
    public class NameGenerator : ScriptableObject {
        [System.Serializable]
        public class Part {
            public PartList list;
            [Range(0, 1)]
            public double skipChance;
            public bool spaceBefore;
        }
        public List<Part> parts;

        public string Next { get {
                return Generate();
            } }

        private string Generate() {
            string result = "";
            for(int i = 0; i < parts.Count; i++) {
                Part part = parts[i];
                if(Random.value < part.skipChance)
                    continue;
                if(part.spaceBefore && result != "")
                    result += " ";
                result += part.list.possible[Random.Range(0, part.list.possible.Count)];
            }
            return result;
        }
    }
}