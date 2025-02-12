using System.Collections.Generic;
using UnityEngine;

namespace OneKnight.OKGraphics {
    [System.Serializable]
    public class Arc : IShape {

        Vector2 a, b;
        public float sweepAngle;
        public float r;
        public float theta0;
        public Vector2 origin;

        public bool IsLine {
            get { return sweepAngle == 0; }
        }

        public bool IsPoint {
            get { return r == 0; }
        }


        public void Set(Vector2 a, Vector2 b, float sweepAngle) {
            this.a = a;
            this.b = b;
            this.sweepAngle = sweepAngle;
            if(IsLine)
                return;
            float ax = a.x;
            float ay = a.y;
            float bx = b.x;
            float by = b.y;
            float d = (a-b).magnitude;
            float halfTheta = .5f*Mathf.Deg2Rad*sweepAngle;
            r = Mathf.Abs(d / (2*Mathf.Sin(halfTheta)));
            if(IsPoint)
                return;
            bool swap = false;
            if(by == ay) {
                swap = true;
                ay = a.x;
                ax = a.y;
                by = b.y;
                bx = b.x;
            }
            float alpha = 2*(bx-ax);
            float beta = 2*(by-ay);
            float gamma = bx*bx - ax*ax + by*by - ay*ay;
            float delta = gamma - beta*ay;
            float A = alpha*alpha/beta/beta+1;
            float B = -2*alpha*delta/beta/beta - 2*ax;
            float C = delta*delta/beta/beta - r*r + ax*ax;
            float sign = Mathf.Sign(by-ay);
            sign = sweepAngle > 180 ? sign : -1*sign;
            float x = Utils.QuadraticFormula(A, B, C, sign);
            float y = (gamma - alpha*x)/beta;
            if(swap) {
                float temp = x;
                x = y;
                y = temp;
            }

            origin.x = x;
            origin.y = y;
            sign = a.y < y ? -1 : 1;
            theta0 = Mathf.Rad2Deg*Mathf.Acos((a.x-x)/r);
            theta0 = (theta0+540)%360-180;
            if(Mathf.Sign(theta0) < 0 != sign < 0)
                theta0 *= -1;
        }

        public Vector3 Sample(float t) {
            if(r == 0) {

            }
            if(IsLine) {
                return Vector2.LerpUnclamped(a, b, t);
            }
            float theta = Mathf.LerpUnclamped(theta0, theta0+sweepAngle, t);
            theta *= Mathf.Deg2Rad;
            return new Vector2(r*Mathf.Cos(theta)+origin.x, r*Mathf.Sin(theta) + origin.y);
        }

        public float ApproximateLength { get {
                return (sweepAngle)*Mathf.Deg2Rad*r;
            }
        }
        public bool Closed { get { return a == b; } }
    }
}