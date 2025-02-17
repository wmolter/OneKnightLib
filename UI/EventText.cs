using UnityEngine;
using System.Collections;
using TMPro;

namespace OneKnight.UI {
    [RequireComponent(typeof(TextManipulator), typeof(EventValueFormatter))]
    public class EventText : MonoBehaviour {

        private void Start() {
            SetGetter();
        }
        private void OnEnable() {
            SetGetter();
        }

        private void SetGetter() {
            GetComponent<TextManipulator>().Permanent(GetComponent<EventValueFormatter>().TextGetter);
        }

    }
}