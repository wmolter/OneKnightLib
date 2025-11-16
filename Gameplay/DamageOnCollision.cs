using UnityEngine;
using System.Collections.Generic;
using OneKnight.Gameplay.Behavior;
using UnityEngine.Events;

namespace OneKnight.Gameplay {
    [RequireComponent(typeof(Health))]
    public class DamageOnCollision : MonoBehaviour {

        public float damage;
        public float interval;
        public AOEHandler filter;
        

        private void OnTriggerEnter2D(Collider2D collider) {
            if(filter.FilterWithLayers(collider))
                GetComponent<Health>().AddOverTime(-damage, interval, collider, collider);
        }

        private void OnTriggerExit2D(Collider2D collider) {
            if(filter.FilterWithLayers(collider))
                GetComponent<Health>().RemoveOverTime(collider);
        }
    }
}