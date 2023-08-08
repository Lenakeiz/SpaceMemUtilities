using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceMem.Patterns.State
{
    public class StateMachine<T>
    {
        readonly T agent;
        State<T> currState = null;

        public State<T> GetState { get { return currState; } set { currState = value; } }

        //contructor
        public StateMachine(T _agent)
        {
            agent = _agent;
        }

        private void SwitchState(State<T> state)
        {
            if (currState == state)
            {
                Debug.LogWarning("Switching to te the same state");
            }

            if (currState != null)
            {
                currState.ExitState(agent);
            }
            currState = state;
            currState.EnterState(agent);
        }

        public void StateUpdate()
        {
            if (currState != null)
            {
                currState.ExecuteState(agent);
            }
        }
    }

}
