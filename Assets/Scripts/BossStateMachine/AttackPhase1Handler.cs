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
    public class AttackPhase1Handler : StateHandler
    {
        private Boss boss;

        private void Awake()
        {
            boss = GetComponentInParent<Boss>();
        }
        public override void OnEnter<T>(T transition)
        {
            // Set the duration of attacks
            boss.attackDuration = 8f;
            // Set the speed of the ball
            boss.ballForce = 100f;

            // Change the sprites / color of sprite

            // Set the attacks that are possible in this state
            boss.possibleActions.Add("GroundedAttack");
            boss.possibleActions.Add("SlamAttack");
            boss.possibleActions.Add("UpDownAttack");
        }

        public override void OnExit<T>(T transition)
        {
            boss.StopAllCoroutines();
            boss.possibleActions.Clear();
        }
    }

}
