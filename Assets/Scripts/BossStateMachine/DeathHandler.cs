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
        [SerializeField] private GameObject boss;
        public override void OnEnter<T>(T transition)
        {
            GameObject.Destroy(boss);
        }

        public override void OnExit<T>(T transition)
        {

        }
    }
}
