using UnityEngine;
using System.Collections.Generic;
using OneKnight.PropertyManagement;

namespace OneKnight {
    [System.Serializable]
    public class Property {

        public PropertyManager Manager {
            get;
            private set;
        }

        public string name;
        public float underlying;
        public float Value {
            get {
                if(Manager == null)
                    return underlying;
                return Manager.AdjustProperty(name, underlying);
            }
        }
        public Property(string name) : this(0, name) { }

        public Property(float val, string name) : this(val, name, null) { }

        public Property(string name, PropertyManager manager) : this(0, name, manager) { }

        public Property(float val, string name, PropertyManager manager) {
            underlying = val;
            this.name = name;
            this.Manager = manager;
        }

        public void Register(PropertyManager manager) {
            if(Manager != null)
                throw new UnityException("property manager cannot be registered twice");
            Manager = manager;
        }

        public static implicit operator float(Property prop) {
            return prop.Value;
        }
    }
}