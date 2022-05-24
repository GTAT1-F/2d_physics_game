using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;
using UnityEngine;

namespace BossStateMachine
{
    public class AttackPhase2Handler : StateHandler
    {
        private Boss boss;
        private SpriteRenderer spriteRenderer;
        private void Awake()
        {
            boss = GetComponentInParent<Boss>();
            spriteRenderer = boss.GetComponent<SpriteRenderer>();
        }
        public override void OnEnter<T>(T transition)
        {
            // Set the duration of attacks
            boss.startingAttackDuration = 5f;
            // Set the speed of the ball
            boss.ballForce = 200f;

            // Change the sprites / color of sprite
            spriteRenderer.color = new Color(0.9f, 0.4f, 0.4f);

            // Set the attacks that are possible in this state
            boss.possibleActions.Add("GroundedAttack");
            boss.possibleActions.Add("SlamAttack");
            boss.possibleActions.Add("UpDownAttack");

            boss.actionsCount = boss.possibleActions.Count;
        }

        public override void OnExit<T>(T transition)
        {
            boss.StopAllCoroutines();
            boss.possibleActions.Clear();
            boss.actionsCount = 0;
        }
    }
}
