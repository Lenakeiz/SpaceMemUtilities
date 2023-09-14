using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMem.UI
{
    public class VRDebugLogCollector
    {
        public delegate void LogEvent(string logString, string stackTrace, LogType type);
        public event LogEvent OnLogReceived;

        public VRDebugLogCollector()
        {
            SubscribeToLog();
        }
        public void SubscribeToLog()
        {
            Application.logMessageReceived += HandleLog;
        }
        public void UnsubscribeFromLog()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            OnLogReceived?.Invoke(logString, stackTrace, type);
        }
    }

}
