using UnityEngine;
using System.Collections;

namespace OneKnight.Cameras {

    [RequireComponent(typeof(Camera))]
    public class CameraResizer : MonoBehaviour {

        public int tileSize = 32;
        
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            GetComponent<Camera>().orthographicSize = Screen.height / 2f / tileSize;
        }
    }
}
