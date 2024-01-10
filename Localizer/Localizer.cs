using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SpaceMem.Localizer
{

    [CreateAssetMenu(fileName = "Localizer", menuName = "Space Memory Utilities/Localization/Localizer")]

    public class Localizer : ScriptableObject
    {
        public string Separator = ";;";
        // Assume factory is initialized somewhere, possibly injected
        public IMessageDataFactory factory;

        public Dictionary<string, IMessageData> LocalizationData = new Dictionary<string, IMessageData>();

        public void AssignFactory(IMessageDataFactory newFactory)
        {
            factory = newFactory;
        }

        public void LoadLocalizationData(string fullfilepath)
        {
            CSVParser<IMessageData> parser = new CSVParser<IMessageData>(factory, ";;");
            LocalizationData = parser.ReadCSV(fullfilepath);
#if UNITY_EDITOR
            PrintAllLocalizedMessages();
#endif
        }
        public void LoadLocalizationData(TextAsset textAsset)
        {
            CSVParser<IMessageData> parser = new CSVParser<IMessageData>(factory, ";;");
            LocalizationData = parser.ReadCSV(textAsset);
#if UNITY_EDITOR
            PrintAllLocalizedMessages();
#endif
        }

        public void CreateDefaultLocalizationData(string fullPath, Dictionary<string, IMessageData> defaultData)
        {
            CSVParser<IMessageData> parser = new CSVParser<IMessageData>(factory, ";;");
            parser.WriteCSV(defaultData, fullPath);
        }

        public IMessageData GetMessage(string id)
        {
            if (LocalizationData.ContainsKey(id))
            {
                return LocalizationData[id];
            }
            else
            {
                Debug.LogWarning($"Localization key {id} not found.");
                return null;
            }
        }

        public void PrintAllLocalizedMessages()
        {
            foreach (KeyValuePair<string, IMessageData> entry in LocalizationData)
            {
                Debug.Log($"Key: {entry.Key}, Message: {entry.Value.ToString()}");
            }
        }
    }
}


