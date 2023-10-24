using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMem.Localizer;
using System.IO;
using SpaceMem.Localizer.Examples;

public class ExampleLocalizerManager : MonoBehaviour
{
    public Localizer localizer;
    public string fileName = "en-UK.csv";
    [Header("Assigned at runtime")]
    public string folderPath = "";


    private string fullFilePath;

    // Start is called before the first frame update
    void Awake()
    {
        folderPath = Path.Combine(Application.persistentDataPath, "Localization");
        // Set the full file path to read the CSV file from the 'Localization' folder
        fullFilePath = Path.Combine(folderPath, fileName);
    }

    private void Start()
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        Dictionary<string, IMessageData> defaultData = new Dictionary<string, IMessageData>
        {
            { ExampleLocalizationKeys.ExampleKey1, new ExampleMessageData("Header1", "Body1", 1.0f) },
            { ExampleLocalizationKeys.ExampleKey2, new ExampleMessageData("Header2", "Body2", 2.0f) }
        };

        localizer.AssignFactory(new ExampleMessageDataFactory());

        // Check if the file exists; if not, create default data
        if (!File.Exists(fullFilePath))
        {
            localizer.CreateDefaultLocalizationData(fullFilePath, defaultData);
        }

        localizer.LoadLocalizationData(fullFilePath);

        // Retrieve a message
        IMessageData message = localizer.GetMessage(ExampleLocalizationKeys.ExampleKey2);
        if (message is ExampleMessageData exampleMessage)
        {
            Debug.Log($"Header: {exampleMessage.Header}, Body: {exampleMessage.Body}");
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
