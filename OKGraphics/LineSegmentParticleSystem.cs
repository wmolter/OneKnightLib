using System.Collections;
using UnityEngine;

namespace OneKnight.OKGraphics {
    [RequireComponent(typeof(ParticleSystem))]
    public class LineSegmentParticleSystem : MonoBehaviour {

        [field:SerializeField]
        public Vector3 a {
            get; set;
        }
        [field: SerializeField]
        public Vector3 b {
            get; set;
        }
        public Transform transA, transB;

        private void LateUpdate() {
            if(transA != null)
                a = transA.position;
            if(transB != null)
                b = transB.position;

            ParticleSystem.ShapeModule shape = GetComponent<ParticleSystem>().shape;
            shape.position = Vector3.Lerp(a, b, .5f);
            shape.radius = Vector3.Distance(a, b)/2;
            Vector3 planeNormal = Vector3.Cross(a-b, Vector3.right);
            float angle = Vector3.Angle(a-b, Vector3.right);
            shape.rotation = Quaternion.AngleAxis(-angle, planeNormal).eulerAngles;
        }
    }
}