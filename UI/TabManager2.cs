using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace OneKnight.UI { 
    [System.Serializable]
    public class TabContent {
        public TabIndicator indicator;
        public GameObject[] inTab;
        public UnityEvent onActivate;
        public UnityEvent onDeactivate;
    }

    public class TabManager2 : MonoBehaviour, ITabManager {
        public int startingTab = 0;
        public TabContent[] tabContents;

        public int currentTab { get; private set; }
        public TabIndicator CurrentIndicator {
            get {
                if(currentTab >= 0 && tabContents != null && currentTab < tabContents.Length && tabContents[currentTab].indicator != null)
                    return tabContents[currentTab].indicator.isActiveAndEnabled ? tabContents[currentTab].indicator : null;
                else
                    return null;
            }
        }

        //
        public void ActivateTab(int index) {
            Debug.Log("Activating tab " + index);
            startingTab = index;
            int prevTab = currentTab;
            currentTab = index;
            for(int i = 0; i < tabContents.Length; i++) {
                bool active = i==currentTab;
                TabContent content = tabContents[i];
                foreach(GameObject obj in content.inTab) {
                    obj.SetActive(active);
                }
                content.indicator?.Indicate(active);
                if(prevTab != currentTab && active) {
                    content.onActivate?.Invoke();
                }
                if(prevTab == i && !active) {
                    content.onDeactivate?.Invoke();
                }
            }
        }


        // Use this for initialization
        void OnEnable() {
            Validate();
            StartCoroutine(Utils.WaitOneFrame(ActivateTab, startingTab));
        }

        public void Validate() {
            int index = 0;
            foreach(TabContent content in tabContents) {
                content.indicator?.Init(this, index);
                index++;
            }
        }
    }
}