using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMem.Localizer;

namespace SpaceMem.Localizer.Examples
{
    // Define the Message Data class, which implements IMessageData
    public class ExampleMessageData : IMessageData
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public float Duration { get; set; }

        public ExampleMessageData() { }

        public ExampleMessageData(string header, string body, float duration)
        {
            Header = header;
            Body = body;
            Duration = duration;
        }

        public string[] SerializeMessageData()
        {
            return new string[] { Header, Body, Duration.ToString("F2") };
        }

        public void DeserializeMessageData(string[] fields)
        {
            if (fields.Length == 3)
            {
                Header = fields[0];
                Body = fields[1];
                Duration = float.TryParse(fields[2], out float result) ? result : 0f;
            }
            else
            {
                Debug.LogError("Message data does not have correct length");
            }
        }

        public string[] GetHeaderFields()
        {
            return new string[] { "Header", "Body", "Duration" };
        }

        public override string ToString()
        {
            return $"Header: {Header}, Body: {Body}, Duration: {Duration}";
        }
    }

    // Define ExampleMessageDataFactory
    public class ExampleMessageDataFactory : IMessageDataFactory
    {
        public IMessageData CreateInstance()
        {
            return new ExampleMessageData();
        }
    }

}