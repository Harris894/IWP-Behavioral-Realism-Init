using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechController : MonoBehaviour
{
    private SpeechRecognition speechRec;

    public string lastMessage="";

    public List<string> greetingKeywords = new List<string>();

    public List<NPC_Controller> activeNPCs = new List<NPC_Controller>();


    void Start()
    {
        speechRec = GetComponent<SpeechRecognition>();

        FindNPCSinScene();
    }

    //Finds all the NPCs in the scene and puts them in a list
    private void FindNPCSinScene()
    {
        activeNPCs.AddRange(GameObject.FindObjectsOfType<NPC_Controller>());
    }

    //Gets the last spoken string of words from the SpeechRecognition script.
    public void ReceiveLatestSpeech()
    {
        lastMessage = speechRec.lastRecognizedString;
        Debug.Log(lastMessage);
        string[] separator = { " " };
        string[] words = lastMessage.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
        //int count = 0;

        //Goes through the 
        //foreach (var word in words)
        //{
        //    if (greetingKeywords.Contains(word)){
        //        Debug.Log("HEARD: " + word);
        //        count++;
        //    }
        //    else
        //    {
        //        Debug.LogError("Combination of keywords was not understood or are not in the dictionary");
        //    }
        //}

        
        Debug.Log(words.Length);

        if (words.Length >= greetingKeywords.Count/2)
        {
            Debug.Log(greetingKeywords.Count / 2);
            activeNPCs[0].InitApproachToCounter();
        }

        
    }



}
