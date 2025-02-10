using UnityEngine;
using System.Collections.Generic;
using OneKnight.Cameras;

namespace OneKnight.OKGraphics {
    public interface IShape {
        Vector3 Sample(float t);
        float ApproximateLength { get; }
        bool Closed { get; }
    }
}
