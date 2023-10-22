using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace SpaceMem.Localizer
{

    public class MessageData
    {
        public string Header { get; set; }
        public string Body { get; set; }

        public float Duration { get; set; }

        public MessageData(string header, string body, float duration)
        {
            Header = header;
            Body = body;
            Duration = duration;
        }
    }
    public class CSVParser
    {
        public static Dictionary<string, MessageData> ReadCSV(string fullpathfile)
        {
            Dictionary<string, MessageData> csvData = new Dictionary<string, MessageData>();

            if (!File.Exists(fullpathfile))
            {
                Debug.LogError("Localization file not found");
                return csvData;
            }

            string[] lines = File.ReadAllLines(fullpathfile);

            //Skipping the header
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] fields = line.Split(new string[] { ";;" }, StringSplitOptions.None);
                // Make sure each line has exactly 4 fields
                if (fields.Length != 4)
                {
                    Debug.LogError($"Malformed line {i}: {line}");
                    continue;
                }

                string id = fields[0].Trim();
                string header = fields[1].Trim();
                string body = fields[2].Trim();
                float duration = float.Parse(fields[3]);

                csvData[id] = new MessageData(header, body, duration);

            }
            return csvData;
        }
        public static void WriteCSV(Dictionary<string, MessageData> data, string filepath)
        {
            StringBuilder sb = new StringBuilder();

            // Adding header line
            sb.AppendLine("ID;;Header;;Body;;Duration");

            foreach (KeyValuePair<string, MessageData> item in data)
            {
                sb.AppendLine(string.Format("{0};;{1};;{2};;{3:F2}", item.Key, item.Value.Header, item.Value.Body, item.Value.Duration));
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(filepath))
                {
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Error writing CSV: " + ex.Message);
            }
        }
    }

}
