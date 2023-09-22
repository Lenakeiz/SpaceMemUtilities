using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceMem.Patterns.Observer
{
    [System.Serializable] public class UnityObjectEvent<TValue> : UnityEvent<TValue>{}

    /// <summary>
    /// Using new Unity Events to dispatch information upon different sources
    /// </summary>
    public static class ObserverDispatcher<TValue>
    {
        private static Dictionary<string, UnityObjectEvent<TValue>> events = new Dictionary<string, UnityObjectEvent<TValue>>();

        public static void AddListener(string eventName, UnityAction<TValue> subscribedMethod)
        {
            if (!events.ContainsKey(eventName))
            {
                events[eventName] = new UnityObjectEvent<TValue>();
            }
            events[eventName].AddListener(subscribedMethod);
        }

        public static void RemoveListener(string eventName, UnityAction<TValue> subscribedMethod)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName].RemoveListener(subscribedMethod);
            }
        }

        public static void RemoveAllListeners(string eventName)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName].RemoveAllListeners();
            }
        }

        public static void Invoke(string eventName, TValue value)
        {
            if (events.ContainsKey(eventName))
            {
#if UNITY_EDITOR
                Debug.Log("Invoking event: " + eventName);
#endif
                events[eventName]?.Invoke(value);
            }
        }
    }
}


