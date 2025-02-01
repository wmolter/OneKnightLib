using System.Collections.Generic;
using UnityEngine;

namespace OneKnight.Generation {
    [CreateAssetMenu(menuName = "Generation/Name Generator/Part List")]
    public class PartList : ScriptableObject{
        public List<string> possible;
    }
}