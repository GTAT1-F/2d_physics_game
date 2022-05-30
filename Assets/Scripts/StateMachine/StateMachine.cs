using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace StateMachine
{
    enum RuntimeTransitions
    {
        FromInit
    }
    public abstract class StateMachine<Transition> : MonoBehaviour where Transition : struct, Enum
    {
        [SerializeField] private List<TransitionConnection> transitions;
        [field: SerializeField] public StateHandler CurrentState { get; private set; }

        private Dictionary<StateHandler, List<TransitionConnection>> stateToTransitions;

        protected void AddTransition(StateHandler from, StateHandler to, Transition transition)
        {
            if (transitions.Any(x => x.from == from && x.transition.Equals(transition)))
            {
                Debug.LogError($"There is already a transition from {from.Name} with transition {transition}.");
                return;
            }

            transitions.Add(new TransitionConnection(from, to, transition));
        }

        protected void Commit(StateHandler optionalEntry = null)
        {
            if(stateToTransitions != null)
            {
                Debug.LogError("State machine is already built.");
                return;
            }

            stateToTransitions = new Dictionary<StateHandler, List<TransitionConnection>>();
            foreach(var transition in transitions)
            {
                if (stateToTransitions.ContainsKey(transition.from))
                {
                    stateToTransitions[transition.from].Add(transition);
                }
                else
                {
                    stateToTransitions[transition.from] = new List<TransitionConnection>() { transition };
                }
            }
            if(optionalEntry != null)
            {
                CurrentState = optionalEntry;
                CurrentState.OnEnter(RuntimeTransitions.FromInit);
            }
            else if(transitions.Count > 0)
            {
                CurrentState = transitions[0].from;
                CurrentState.OnEnter(RuntimeTransitions.FromInit);
            }
        }

        public bool Trigger(Transition transition)
        {
            if (stateToTransitions == null) 
            {
                return false;
            }
            var possibleTransitions = stateToTransitions[CurrentState];
            var concreteTransition = possibleTransitions.FirstOrDefault(x => x.transition.Equals(transition));
            if(concreteTransition == null) { return false; }
            // Debug.Log($"[{concreteTransition.from.Name}].OnExit() => [{concreteTransition.to.Name}].OnEnter()");
            concreteTransition.from.OnExit(transition);
            CurrentState = concreteTransition.to;
            concreteTransition.to.OnEnter(transition);
            return true;
        }

        protected virtual void Start()
        {
            if(stateToTransitions != null) { return; }
            Commit();
        }

        [Serializable] protected class TransitionConnection
        {
            public Transition transition;
            public StateHandler from;
            public StateHandler to;

            public TransitionConnection(StateHandler from, StateHandler to, Transition transition)
            {
                this.from = from;
                this.to = to;
                this.transition = transition;
            }
        }
    }
}
