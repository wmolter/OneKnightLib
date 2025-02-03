using UnityEngine;
using System.Collections;

namespace OneKnight.OKGraphics {
    public abstract class Ellipse {
        public abstract Vector3 EvenDistributionPosition(float theta);
        //public abstract Quaternion RotationLocal(float theta);

        public abstract float ApproximatePerimeter { get; }
    }
}
