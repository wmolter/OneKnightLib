using System.Collections;
using UnityEngine;

namespace OneKnight.OKGraphics {
    [System.Serializable]
    public class Circle : IShape {

        public float r;

        public Circle(float r) {
            this.r = r;
        }
        public Vector3 Sample(float t) {
            return new Vector3(r*Mathf.Cos(t*Mathf.PI*2), r*Mathf.Sin(t*Mathf.PI*2));
        }

        public float ApproximateLength { get {
                return 2*Mathf.PI*r;
            }
        }
        public bool Closed { get { return true; } }
    }
}