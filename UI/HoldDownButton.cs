using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace OneKnight.UI {
    public class HoldDownButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler {

        public bool IsDown { get; private set; }
        public UnityEvent OnDown;
        public UnityEvent OnUp;

        void StartHold() {
            IsDown = true;
            OnDown.Invoke();
        }

        void EndHold() {
            if(IsDown) {
                IsDown = false;
                OnUp.Invoke();
            }
        }

        public void OnPointerDown(PointerEventData data) {
            StartHold();
        }

        public void OnPointerUp(PointerEventData data) {
            EndHold();
        }

        public void OnPointerExit(PointerEventData data) {
            EndHold();
        }
    }
}