using UnityEngine;
using System.Collections;
using TMPro;

namespace OneKnight.UI {
    [RequireComponent(typeof(TextManipulator), typeof(DynamicValueFormatter))]
    public class DynamicText : MonoBehaviour {

        private void Start() {
            SetGetter();
        }
        private void OnEnable() {
            SetGetter();
        }

        private void SetGetter() {
            GetComponent<TextManipulator>().Permanent(GetComponent<DynamicValueFormatter>().TextGetter);
        }

    }
}