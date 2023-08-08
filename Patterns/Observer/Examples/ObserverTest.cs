using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SpaceMem.Patterns.Observer;
using SpaceMem.Patterns.Observer.Examples;

public class ObserverTest : MonoBehaviour
{
    public const string GAMEOBJECT_UPDATE_POS_EVENT = "GAMEOBJECT_UPDATE_POS_EVENT";
    void Start()
    {
        ObserverDispatcher<GameObejctUpdatePosEvent>.AddListener(GAMEOBJECT_UPDATE_POS_EVENT,RewardHitPosition);
    }

    private void OnDestroy()
    {
        ObserverDispatcher<GameObejctUpdatePosEvent>.RemoveAllListeners(GAMEOBJECT_UPDATE_POS_EVENT);
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Sending event");
            ObserverDispatcher<GameObejctUpdatePosEvent>.Invoke(GAMEOBJECT_UPDATE_POS_EVENT,new GameObejctUpdatePosEvent() {callingPosition = new Vector3(1,2,3)});
        }
    }

    public void RewardHitPosition(GameObejctUpdatePosEvent mei)
    { 
        Debug.Log("The magic has happened!");
        Debug.Log("Position x" + mei.callingPosition.x);
    }

}
