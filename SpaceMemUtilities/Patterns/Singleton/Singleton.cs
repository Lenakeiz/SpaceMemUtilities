﻿using UnityEngine;
using System.Collections;

namespace SpaceMem.Patterns.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        //preventing concurrent call to the instance
        private static object _lock = new object();

        private static bool applicationQuitting = false;

        public static T Instance
        {
            get
            {
                if (applicationQuitting)
                    return null;

                lock (_lock)
                {
                    if (_instance == null)
                    {

                        _instance = (T)GameObject.FindObjectOfType(typeof(T));
                        //double checking for other instance
                        if (GameObject.FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError("Found more than one singleton instance");
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();

                            singleton.name = "Singleton " + typeof(T).Name;

                            DontDestroyOnLoad(singleton);
                        }

                    }

                    return _instance;
                }


            }
        }

        protected virtual void Start()
        {

        }

        protected virtual void Awake()
        {

        }

        //overrinding OnDestroy Unity function
        public virtual void OnDestroy()
        {
            applicationQuitting = true;
        }
    }

}
