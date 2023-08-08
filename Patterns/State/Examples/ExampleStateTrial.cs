using SpaceMem.Patterns.State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceMem.Patterns.State.Examples
{

    public class ExampleStateMachine : MonoBehaviour
    {
        protected StateMachine<ExampleStateMachine> m_fsm;
        [SerializeField] public string CurrentStateName { get; set; }

        #region Controller Methods
        public virtual void SwitchState(ExampleTrialState state)
        {
            if (m_fsm.GetState != null && state.Name == m_fsm.GetState.Name)
            {
                Debug.LogWarning("Switching to the same state: " + state.Name);
            }

            if (m_fsm.GetState != null)
            {
                var exitState = m_fsm.GetState;
                exitState.ExitState(this);
                //Destroy
                var exitType = exitState.GetType();
                Destroy(gameObject.GetComponent(exitType));
            }

            var currtype = state.GetType();
            var currState = gameObject.AddComponent(currtype) as ExampleTrialState;
            m_fsm.GetState = currState;
            currState.EnterState(this);

        }
        #endregion

        #region Unity_Methods

        public ExampleStateMachine()
        {
            m_fsm = new StateMachine<ExampleStateMachine>(this);
        }

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            m_fsm.StateUpdate();
            if (m_fsm.GetState != null)
                CurrentStateName = m_fsm.GetState.Name;
        }
        #endregion

        #region Events
        //Place events here

        //Place event triggers here
        #endregion

    }
}
