using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceMem.Patterns.State
{
    public interface State<T>
    {
        string Name { get; set; }
        void EnterState(T agent);
        void ExecuteState(T agent);
        void ExitState(T agent);
    }

}
