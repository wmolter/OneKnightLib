using UnityEngine;
using System.Collections;

namespace OneKnight.Gameplay.Behavior {
    [System.Serializable]
    public struct Stun : IAttackEffect {
        public float duration;
        public void Attack(Health opponent, Component self) {
            Stunnable s = opponent.GetComponent<Stunnable>();
            if(s != null && s.enabled) {
                s.Stun(duration);
            }
        }
    }
}