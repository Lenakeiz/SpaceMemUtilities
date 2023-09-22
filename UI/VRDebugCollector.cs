using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SpaceMem.UI
{
    public class VRDebugLogCollector
    {
        public delegate void LogEvent(string logString, string stackTrace, LogType type);
        public event LogEvent OnLogReceived;

        private readonly string filePath;

        public VRDebugLogCollector()
        {
            filePath = Path.Combine(Application.persistentDataPath, "log.txt");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            File.Create(filePath).Dispose();
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
            
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{DateTime.Now} [{type}] {logString}");
                if (type == LogType.Error || type == LogType.Exception)
                {
                    writer.WriteLine($"StackTrace: {stackTrace}");
                }
            }
        }
    }

}
