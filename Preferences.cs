using UnityEngine;
using System.Collections.Generic;
using OneKnight.Loading;

namespace OneKnight {
    public static class Preferences {

        public const string filename = "preferences.txt";
        private static Dictionary<string, object> defaults;
        private static Dictionary<string, object> prefs;
        private static List<string> changed;

        public static void Load() {
            prefs = new Dictionary<string, object>();
            SavingUtils.ReadDict(SavingUtils.ReadOKTable(Application.dataPath + "/" + filename).GetEnumerator(), prefs);
            Language = (string)prefs["language"];
        }

        public static void Save() {
            SavingUtils.WriteDictEntries(filename, prefs, changed);
        }

        public static void SetDefault(string name, object value) {
            if(defaults == null) {
                defaults = new Dictionary<string, object>();
            }
            defaults[name] = value;
        }

        public static void SetDefaults(Dictionary<string, object> defaults) {
            Preferences.defaults = defaults;
        }

        public static string LanguageFileModifier { get { return Language; } }

        public static string Language {
            get; set;
        }

        public static bool Has(string name) {
            return prefs.ContainsKey(name);
        }

        public static bool DefaultHas(string name) {
            return defaults.ContainsKey(name);
        }

        public static bool GetToggle(string name) {
            return (bool)prefs[name];
        }

        public static bool GetDefaultToggle(string name) {
            return (bool)defaults[name];
        }

        public static bool GetToggleSafe(string name) {
            if(Has(name))
                return GetToggle(name);
            if(DefaultHas(name))
                return GetDefaultToggle(name);
            return false;
        }

        public static object Get(string name) {
            return prefs[name];
        }

        public static object GetDefault(string name) {
            return defaults[name];
        }

        public static object GetSafe(string name) {
            if(Has(name))
                return Get(name);
            return GetDefault(name);
        }

        public static T Get<T>(string name) {
            return (T)prefs[name];
        }

        public static T GetDefault<T>(string name) {
            return (T)defaults[name];
        }

        public static T GetSafe<T>(string name) {
            if(Has(name))
                return Get<T>(name);
            return GetDefault<T>(name);
        }

        public static void UpdateAdd(string name, object value) {
            prefs[name] = value;
            if(changed == null) {
                changed = new List<string>();
            }
            changed.Add(name);
        }
    }
}