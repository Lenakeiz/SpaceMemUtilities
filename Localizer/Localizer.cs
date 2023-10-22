using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SpaceMem.Localizer
{
    [CreateAssetMenu(fileName = "Localizer", menuName = "SpaceMemoryUtilities/Localization/Localizer")]

    public class Localizer : ScriptableObject
    {
        public Dictionary<string, MessageData> LocalizationData = new Dictionary<string, MessageData>();

        public void LoadLocalizationData(string path)
        {
            LocalizationData = CSVParser.ReadCSV(path);

#if UNITY_EDITOR
            PrintAllLocalizedMessages();
#endif

        }

        public void CreateDefaultLocalizationData(string fullPath)
        {
            Dictionary<string, MessageData> defaultData = new Dictionary<string, MessageData>()
            {
                { MessageIds.FixedStartingPanels               , new MessageData("Welcome adventurer!", "\\u2022<indent=2em>Enter the indicated circle marker</indent>\\\\r\\\\n\\\\r\\\\n\\u2022<indent=2em>Align with the marker direction</indent>\\\\r\\\\n\\\\r\\\\n\\u2022<margin-right=10em><indent=2em>Press the trigger button</indent>",0.0f) },
                { MessageIds.WelcomeMessage                    , new MessageData("Welcome adventurer!", "As your guide, I'll navigate you through this task's tutorial. Please, take care to read the instructions and don't hesitate to direct any queries to the researcher.",15.0f) },
                { MessageIds.Tutorial_1                        , new MessageData("Tutorial", "Let's begin by familiarizing ourselves with the controller. Please PRESS the TRIGGER button ONCE.",0.0f) },
                { MessageIds.Tutorial_2                        , new MessageData("Tutorial", "Excellent! In this task, you'll need to remember and then reposition one (or more) object(s). Let's walk through an example to make it clear. When you're ready, Press the trigger button once to proceed with the instructions.",0.0f) },
                { MessageIds.Tutorial_Trial_1                  , new MessageData("Tutorial", "An object will soon appear in the highlighted green area ahead. Your task is to remember its position. Seek out the timer that will show you how much time you have left to memorize this.",15.0f) },
                { MessageIds.Tutorial_Trial_2                  , new MessageData("Tutorial", "Got it memorized? Great! Press the trigger button once to proceed with the instructions.",0.0f) },
                { MessageIds.Tutorial_Trial_3                  , new MessageData("Tutorial", "Next, you'll be asked to reposition the object you've just memorized. This will be done using a laser pointer, set to activate shortly. You're free to direct this laser wherever you choose.", 15.0f) },
                { MessageIds.Tutorial_Pointer_Explanation      , new MessageData("Tutorial", "Can you see how the laser changes color based on where you're pointing? Give it a try on the floor outside the highlighted area. Notice how it turns red to indicate an invalid position.",20.0f) },
                { MessageIds.Tutorial_Replacement_1            , new MessageData("Tutorial", "You are now free to lock in the object's position by pressing the trigger. Remember, you only get one shot at this!",0.0f) },
                { MessageIds.Tutorial_Replacement_2            , new MessageData("Tutorial", "Well done!",5.0f) },
                { MessageIds.Tutorial_Replacement_Feedback     , new MessageData("Tutorial", "To help you understand any error in repositioning, I'll display the correct position once more for a brief 5 seconds.",10.0f) },
                { MessageIds.Tutorial_Teleport_1               , new MessageData("Tutorial", "I think you got it! Notice that you will not have feedback during the real task. There's one more element to learn: at times, I might ask you to change your location.", 15.0f) },
                { MessageIds.Tutorial_Teleport_2               , new MessageData("Tutorial", "Relocating during this task happens in two ways - either by walking, when indicated, or through 'TELEPORTATION'. Now, let's explore how 'TELEPORTATION' works. PRESS the FRONTPAD button to proceed with the instructions.",0.0f) },
                { MessageIds.Tutorial_Teleport_3               , new MessageData("Tutorial", "Across the area, there's another marked spot. This will be your teleportation destination once activated. For now, press the frontpad button to proceed with the instructions.",0.0f) },
                { MessageIds.Tutorial_Teleport_4               , new MessageData("Tutorial", "Once teleportation is active, you'll be able to engage it by HOLDING DOWN the FRONTPAD button, aiming it at the other floor marker, and then releasing it. For now, press the frontpad to proceed with the instructions.",0.0f) },
                { MessageIds.Tutorial_Teleport_5               , new MessageData("Tutorial", "A laser color will serve you as a guide when holding down the FRONTPAD button. If the laser tip lands WITHIN the floor marker, it will turn green. For now, press the frontpad to proceed with the instructions.",0.0f) },
                { MessageIds.Tutorial_Teleport_Activation      , new MessageData("Tutorial", "Press and hold the FRONTPAD button, aim the controller at the other marker, then release the button. Teleportation is now active.",0.0f) },
                { MessageIds.Tutorial_Teleport_Explanation     , new MessageData("Tutorial", "Awesome! You've successfully relocated using teleportation. Get a sense of your location from this new viewpoint. When ready, I'll guide you to teleport back to where you started.",0.0f) },
                { MessageIds.Tutorial_Preparing_Walking        , new MessageData("Tutorial", "You've mastered teleportation now. There's just one more thing to cover. At times, you'll be required to change your location simply by walking.", 0.0f) },
                { MessageIds.Tutorial_Arrows_1                 , new MessageData("Tutorial", "Walking will be prompted by a blinking pathway that will appear at your side. Though it suggests a relocation on foot, for this tutorial, you won't need to actually walk. Press the trigger to proceed with the instructions.", 0.0f) },
                { MessageIds.Tutorial_Arrows_2                 , new MessageData("Tutorial", "I'll activate the blinking pathway on the floor to one side when this message vanishes - just take a look at it. It will stay lit for 10 seconds.", 15.0f) },
                { MessageIds.Tutorial_Arrows_3                 , new MessageData("Tutorial", "Excellent! Keep in mind, we'll use the blinking path to indicate when you should walk between floor markers, or at times, to walk back and forth across the same marker. Press the trigger to continue with the instructions.", 0.0f) },
                { MessageIds.Practice_Ending                   , new MessageData("Tutorial", "This concludes our tutorial. Next, I'll instruct you to walk to another location. After that, if you wish, we can take a brief pause before starting the experiment. Press the trigger to proceed.", 0.0f) },
                { MessageIds.Trial_Set_Objects                 , new MessageData("Task", "Memorize the locations of {0} objects. You have {1} seconds.", 5.0f) },
                { MessageIds.Trial_Change_Side_SameViewpoint   , new MessageData("Task", "Walk away from this marked location and then return, following the blinking path.", 5.0f) },
                { MessageIds.Trial_Change_Side_OtherViewpoint  , new MessageData("Task", "Please walk to the other marked location, following the blinking path.", 5.0f) },
                { MessageIds.Trial_Change_Side_WalkEnding      , new MessageData("Task", "Once correctly located and aligned with the arrows, press the trigger button.", 0.0f) },
                { MessageIds.Trial_Change_Side_WalkComplete    , new MessageData("Task", "Please walk straight to the marked location. No blinking path will be provided at this time. Once in position and aligned with the arrows, press the trigger button.", 5.0f) },
                { MessageIds.Trial_Change_Side_Teleport        , new MessageData("Task", "Use teleportation, hold the FRONTPAD, aim at the other floor marker, and release.", 5.0f) },
                { MessageIds.Trial_Change_Side_Teleport_Back   , new MessageData("Task", "Stand by. Automatic teleportation to the initial marker location will commence shortly. Please remain still.", 5.0f) },
                { MessageIds.Trial_Replace_Object              , new MessageData("Task", "Reposition the object using the laser pointer.", 3.0f) },
                { MessageIds.Trial_Replace_End                 , new MessageData("Task", "Excellent work! You've successfully repositioned all objects.", 5.0f) },
                { MessageIds.Trial_Please_Wait                 , new MessageData("Task", "Please Wait.", 2.5f) },
                { MessageIds.Between_Blocks                    , new MessageData("Intermission", "Please move to the marked location. Feel free to take a break now, just let the researcher know. If you're ready to proceed, press the trigger to enter the next phase.",10.0f) },
                { MessageIds.Ending                            , new MessageData("The End!", "Well done, you've made it to the end! A big thank you for being a part of this. Now, speak to the researcher to help us wrap things up.", 20.0f) }

            };
            CSVParser.WriteCSV(defaultData, fullPath);
        }

        public MessageData GetMessage(string id)
        {
            if (LocalizationData.TryGetValue(id, out MessageData message))
            {
                return message;
            }
            else
            {
                Debug.LogWarning("Message ID not found in localization data: " + id);
                return null;
            }
        }

        public void PrintAllLocalizedMessages()
        {
            foreach (KeyValuePair<string, MessageData> entry in LocalizationData)
            {
                Debug.Log(entry.Key + ": " + entry.Value.Header + " -- " + entry.Value.Body);
            }
        }

    }

    public static class MessageIds
    {
        public const string FixedStartingPanels = "FixedStartingPanels";
        public const string WelcomeMessage = "WelcomeMessage";
        public const string Tutorial_1 = "Tutorial_1";
        public const string Tutorial_2 = "Tutorial_2";
        public const string Tutorial_Trial_1 = "Tutorial_Trial_1";
        public const string Tutorial_Trial_2 = "Tutorial_Trial_2";
        public const string Tutorial_Trial_3 = "Tutorial_Trial_3";
        public const string Tutorial_Pointer_Explanation = "Tutorial_Pointer_Explanation";
        public const string Tutorial_Replacement_1 = "Tutorial_Replacement_1";
        public const string Tutorial_Replacement_2 = "Tutorial_Replacement_2";
        public const string Tutorial_Replacement_Feedback = "Tutorial_Replacement_Feedback";
        public const string Tutorial_Teleport_1 = "Tutorial_Teleport_1";
        public const string Tutorial_Teleport_2 = "Tutorial_Teleport_2";
        public const string Tutorial_Teleport_3 = "Tutorial_Teleport_3";
        public const string Tutorial_Teleport_4 = "Tutorial_Teleport_4";
        public const string Tutorial_Teleport_5 = "Tutorial_Teleport_5";
        public const string Tutorial_Teleport_Activation = "Tutorial_Teleport_Activation";
        public const string Tutorial_Teleport_Explanation = "Tutorial_Teleport_Explanation";
        public const string Tutorial_Preparing_Walking = "Tutorial_Preparing_Walking";
        public const string Tutorial_Arrows_1 = "Tutorial_Arrows_1";
        public const string Tutorial_Arrows_2 = "Tutorial_Arrows_2";
        public const string Tutorial_Arrows_3 = "Tutorial_Arrows_3";
        public const string Practice_Ending = "Practice_Ending";
        public const string Trial_Set_Objects = "Trial_Set_Objects";
        public const string Trial_Change_Side_SameViewpoint = "Trial_Change_Side_SameViewpoint";
        public const string Trial_Change_Side_WalkComplete = "Trial_Change_Side_WalkComplete";
        public const string Trial_Replace_Object = "Trial_Replace_Object";
        public const string Trial_Replace_End = "Trial_Replace_End";
        public const string Trial_Please_Wait = "Trial_Please_Wait";
        public const string Trial_Change_Side_OtherViewpoint = "Trial_Change_Side_OtherViewpoint";
        public const string Trial_Change_Side_WalkEnding = "Trial_Change_Side_WalkEnding";
        public const string Trial_Change_Side_Teleport = "Trial_Change_Side_Teleport";
        public const string Trial_Change_Side_Teleport_Back = "Trial_Change_Side_Teleport_Back";
        public const string Between_Blocks = "Between_Blocks";
        public const string Ending = "Ending";
    }

}


