using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;
using UnityEngine;

namespace BossStateMachine
{
    public class IdleHandler : StateHandler
    {
        Boss boss;
        public void Awake()
        {
            boss = GetComponentInParent<Boss>();
        }
        public override void OnEnter<T>(T transition)
        {
            boss.possibleActions.Clear();
        }

        public override void OnExit<T>(T transition)
        {

        }
    }
}
