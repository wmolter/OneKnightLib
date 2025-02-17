using UnityEngine;
using System.Collections.Generic;

namespace OneKnight.UI {
    public class EventValueFormatter : MonoBehaviour {

        public string formatKey;
        public string format { get; set; }
        public List<EventValueGetter> getters;
        
        public string Text { get {
                if(format == null)
                    format = Strings.Get(formatKey);
                object[] values = new object[getters == null ? 0 : getters.Count];
                for(int i = 0; i < values.Length; i++) {
                    values[i] = getters[i].Get();
                }
                return string.Format(format, values);
            } }

        public TextGetter TextGetter {
            get {
                return GetText;
            }
        }
        

        public string GetText() {
            return Text;
        }
    }
}