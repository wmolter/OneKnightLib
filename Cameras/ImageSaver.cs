using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace OneKnight.Cameras {

    [RequireComponent(typeof(Camera))]
    public class ImageSaver : MonoBehaviour {

        public delegate void CopyCallback(Texture2D tex);

        public int width, height;
        public RenderTexture texture;
        private Rect textureSize;
        public UnityEvent<Texture2D> onCopy;
        public bool shouldCopy;
        private List<CopyCallback> callbacks;
        // Use this for initialization
        void Start() {
            texture = new RenderTexture(width, height, 0);
            textureSize = new Rect(0, 0, width, height);
            GetComponent<Camera>().targetTexture = texture;
            Camera.onPostRender += PostRenderCallback;
        }

        public void Copy(CopyCallback callback){
            if(callbacks == null)
                callbacks = new List<CopyCallback>();
            callbacks.Add(callback);
            shouldCopy = true;
        }


        public Texture2D CopyCurrent() {
            Texture2D copy = new Texture2D(width, height);
            copy.ReadPixels(textureSize, 0, 0);
            return copy;
        }

        private void PostRenderCallback(Camera cam) {
            if(cam == GetComponent<Camera>()) {
                if(shouldCopy) {
                    Debug.Log("Capturing from camera: " + cam);
                    Texture2D result = CopyCurrent();
                    result.Apply();
                    shouldCopy = false;
                    if(callbacks != null) {
                        foreach(CopyCallback callback in callbacks) {
                            callback(result);
                        }
                        callbacks.Clear();
                    }
                    onCopy.Invoke(result);
                }
            }
        }
        // Update is called once per frame
        void Update() {
        }
    }
}
