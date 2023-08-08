using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMem.Patterns.Observer.Examples
{
    /// <summary>
    /// Event to update the position of an object inside a single task.
    /// </summary>
    [System.Serializable]
    public class GameObejctUpdatePosEvent
    {
        public GameObject callingGo;
        public int callingID;
        public UnityEngine.Vector3 callingPosition;
    }

    /// <summary>
    /// Event to notify of a reward direction in creation/destruction. The event must be rised before destroyin a reward. 
    /// </summary>
    [System.Serializable]
    public class GameObjectRewardSharing
    { 
        public GameObject rewardSharing;
        public bool toDelete;
    }

}
