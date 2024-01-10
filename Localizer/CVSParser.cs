using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SpaceMem.Localizer
{
    public interface IMessageData
    {
        string[] SerializeMessageData();
        void DeserializeMessageData(string[] fields);
        string[] GetHeaderFields();
        string ToString();
    }

    public interface IMessageDataFactory
    {
        IMessageData CreateInstance();
    }

    public interface IMessageIDProvide
    {
        string GetID(string key);
    }

    public class CSVParser<T> where T : IMessageData
    {
        private readonly string _separator;
        private readonly IMessageDataFactory _factory;

        public CSVParser(IMessageDataFactory factory, string separator = ";;")
        {
            _separator = separator;
            _factory = factory;
        }

        public Dictionary<string, T> ReadCSV(string fullpathfile)
        {
            Dictionary<string, T> csvData = new Dictionary<string, T>();

            if (!File.Exists(fullpathfile))
            {
                Debug.LogError("Localization file not found");
                return csvData;
            }

            string[] lines = File.ReadAllLines(fullpathfile);

            // Skipping the header
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] fields = line.Split(new string[] { _separator }, StringSplitOptions.None);

                string id = fields[0].Trim();

                T messageData = (T)_factory.CreateInstance();
                messageData.DeserializeMessageData(fields.Skip(1).ToArray());
                csvData[id] = messageData;
            }
            return csvData;
        }

        public Dictionary<string, T> ReadCSV(TextAsset textAsset)
        {
            Dictionary<string, T> csvData = new Dictionary<string, T>();

            if (textAsset == null)
            {
                Debug.LogError("TextAsset is null");
                return csvData;
            }

            string[] lines = textAsset.text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Skipping the header
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] fields = line.Split(new string[] { _separator }, StringSplitOptions.None);

                string id = fields[0].Trim();

                T messageData = (T)_factory.CreateInstance();
                messageData.DeserializeMessageData(fields.Skip(1).ToArray());
                csvData[id] = messageData;
            }
            return csvData;
        }

        public void WriteCSV(Dictionary<string, T> data, string filepath)
        {
            StringBuilder sb = new StringBuilder();
            // If the data dictionary is not empty, we'll use the first element to get the header fields
            if (data.Count > 0)
            {
                string[] headers = data.First().Value.GetHeaderFields();
                string headerLine = $"ID{_separator}{string.Join(_separator, headers)}";
                sb.AppendLine(headerLine);
            }

            foreach (KeyValuePair<string, T> item in data)
            {
                string[] serializedData = item.Value.SerializeMessageData();
                string serializedLine = string.Join(_separator, serializedData);
                sb.AppendLine($"{item.Key}{_separator}{serializedLine}");
            }

            try
            {
                Debug.Log(sb.ToString());
                using (StreamWriter sw = new StreamWriter(filepath))
                {
                    
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error writing CSV: {ex.Message}");
            }
        }

    }
}
