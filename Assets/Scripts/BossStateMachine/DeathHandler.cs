using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;
using UnityEngine;

namespace BossStateMachine
{
    public class DeathHandler : StateHandler
    {
        [SerializeField] private Boss boss;

        private void Awake()
        {
            boss = GetComponentInParent<Boss>();
        }
        public override void OnEnter<T>(T transition)
        {
            GameObject.Destroy(boss.gameObject);
         
        }

        public override void OnExit<T>(T transition)
        {
 
        }
    }
}
