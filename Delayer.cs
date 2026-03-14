using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace OneKnight {
    public class Delayer : MonoBehaviour {

        public UnityEvent AtEndOfFrame;
        // Use this for initialization
        void Start() {

        }

        public void Activate() {
            if(AtEndOfFrame.GetPersistentEventCount() > 0) {
                StartCoroutine(FrameEnd());
            }
        }

        IEnumerator FrameEnd() {
            yield return new WaitForEndOfFrame();
            AtEndOfFrame.Invoke();
        }

        // Update is called once per frame
        void Update() {

        }
    }
}