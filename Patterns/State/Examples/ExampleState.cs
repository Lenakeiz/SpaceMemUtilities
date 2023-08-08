using SpaceMem.Patterns.State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMem.Patterns.State.Examples
{
    public class ExampleTrialState : MonoBehaviour, State<ExampleStateMachine>
    {

        public string Name { get; set; }

        public virtual void EnterState(ExampleStateMachine agent)
        {
            if (Application.isEditor)
            {
                Debug.Log("<color=teal>FSM state</color>: " + Name);
            }
        }

        public virtual void ExecuteState(ExampleStateMachine agent)
        {

        }

        public virtual void ExitState(ExampleStateMachine agent)
        {
        }

        #region Unity_Methods
        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void Awake()
        {

        }
        #endregion Unity_Methods

        #region Events           

        #endregion

        public override string ToString()
        {
            return "State: " + Name;
        }


    }
}