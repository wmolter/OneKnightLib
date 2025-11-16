using UnityEngine;
using System.Collections.Generic;

namespace OneKnight.Gameplay.Behavior {
    [CreateAssetMenu(menuName = "Behavior/Melee Attack")]
    public class MeleeAttack : Attack {

        protected new class Act : Attack.Act {
            private MeleeAttack Data { get { return (MeleeAttack)data; } }
            public Act(MeleeAttack data, ActiveNode parent, int index) : base(data, parent, index) {

            }

            protected override void Attack() {
                if(EnemyInRange()) {
                    ApplyAttack(info.main.behaviorTarget.GetComponent<Health>(), info.main);
                }
            }

            protected void ApplyAttack(Health opponent, Component self) {
                if(Data.effects != null)
                    Data.effects.Attack(opponent, self);
                if(Data.damage != 0)
                    new Damage() { amount=Data.damage, ignoreArmor=Data.ignoreArmor, count=1 }.Attack(opponent, self);
            }

        }
        
        public float damage;
        public bool ignoreArmor;

        public AttackEffect effects;

        protected override ActiveNode CreateActive(ActiveNode parent, int index) {
            return new Act(this, parent, index);
        }
    }

}