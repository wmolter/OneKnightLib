using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace OneKnight.UI {
    public class ScreenPosTransformer : MonoBehaviour {

        public UnityEvent<Vector3> OnPoint;
        Vector2 pointPos;
        public void PointEventHandler(InputAction.CallbackContext ctx) {
            Vector2 pos = ctx.ReadValue<Vector2>();
            pointPos = pos;
        }

        private void LateUpdate() {
            OnPoint.Invoke(Camera.main.ScreenToWorldPoint(pointPos));
        }
    }
}