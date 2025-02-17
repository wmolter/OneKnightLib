using System.Collections;
using UnityEngine;

namespace OneKnight.UI {
    public class EventValueGetter : MonoBehaviour {

        private object value;

        public void SetDouble(double d) {
            value = d;
        }

        public void SetFloat(float f) {
            value = f;
        }

        public void SetInt(int i) {
            value = i;
        }

        public void SetObject(object o) {
            value = o;
        }

        public void SetString(string s) {
            value = s;
        }

        public object Get() {
            return value;
        }
    }
}