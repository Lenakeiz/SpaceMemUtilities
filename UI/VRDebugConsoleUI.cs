using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceMem.UI
{
    public class VRDebugConsoleUI : MonoBehaviour
    {
        VRDebugLogCollector _logCollector;

        [Header("Assigned from inspector")]
        public TextMeshProUGUI _debugText;
        Dictionary<string, string> debugLogs = new Dictionary<string, string>();
        
        private void Awake()
        {
            _logCollector = new VRDebugLogCollector();
            _logCollector.OnLogReceived += UpdateUI;
        }

        private void Start()
        {
            if (_debugText == null)
            { 
                Debug.LogError("Debug text not found in VR DEbug Console UI"); 
            }
        }

        private void UpdateUI(string logString, string stackTrace, LogType type)
        {
            if (_debugText == null) 
                return;

            if (type == LogType.Error || type == LogType.Exception)
            {
                string[] splitString = logString.Split(char.Parse(":"));
                string debugKey = splitString[0];
                string debugValue = splitString.Length > 1 ? splitString[1] : "";

                if (debugLogs.ContainsKey(debugKey))
                    debugLogs[debugKey] = debugValue;
                else
                    debugLogs.Add(debugKey, debugValue);

            }

            string displayText = "";
            foreach (KeyValuePair<string, string> log in debugLogs) 
            {
                if (log.Value == "")
                    displayText += log.Key + "\n";
                else
                    displayText += log.Key + ": " + log.Value + "\n";
            }

            _debugText.text = displayText;

        }

        private void OnDestroy()
        {
            _logCollector.OnLogReceived -= UpdateUI;
            _logCollector.UnsubscribeFromLog();
        }
    }

}
