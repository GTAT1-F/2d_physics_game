using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossStateMachine
{
    public enum BossTransitions
    {
        Idle,
        AttackPhase1,
        AttackPhase2,
        Death
    }
}
