using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;
using UnityEngine;

namespace BossStateMachine
{
    public class BossStateMachine : StateMachine<BossTransitions>
    {
        [field: SerializeField] public StateHandler IdleHandler { get; private set; }
        [field: SerializeField] public StateHandler AttackPhase1Handler { get; private set; }
        [field: SerializeField] public StateHandler AttackPhase2Handler { get; private set; }
        [field: SerializeField] public StateHandler DeathHandler { get; private set; }

        private void Awake()
        {
            // Idle -> AttackPhase1; AttackPhase1 -> Idle
            AddTransition(IdleHandler, AttackPhase1Handler, BossTransitions.AttackPhase1);
            AddTransition(AttackPhase1Handler, IdleHandler, BossTransitions.Idle);

            // Idle -> AttackPhase2; AttackPhase2 -> Idle
            AddTransition(IdleHandler, AttackPhase2Handler, BossTransitions.AttackPhase2);
            AddTransition(AttackPhase2Handler, IdleHandler, BossTransitions.Idle);

            // AttackPhase1 -> AttackPhase2
            AddTransition(AttackPhase1Handler, AttackPhase2Handler, BossTransitions.AttackPhase2);

            // AttackPhase2 -> Dead
            AddTransition(AttackPhase2Handler, DeathHandler, BossTransitions.Death);

            Commit();
        }
    }
}
