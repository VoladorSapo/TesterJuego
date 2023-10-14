using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogueList : MonoBehaviour
{
    TextAsset textDialogue;
    TextAsset textMessages;
    Dictionary<string,DialogueClass> DialogueHash;
    Dictionary<string, MessageClass> MessageHash;

    // Start is called before the first frame update
    void Start()
    {
       string[] strings = textDialogue.text.Split(';');
        for (int i = 0; i < strings.Length; i++)
        {
            string[] messagestring = strings[i].Split(',');
            MessageClass message = new MessageClass(messagestring[1], int.Parse(messagestring[2]), int.Parse(messagestring[3]), int.Parse(messagestring[4]),Convert.ToBoolean(messagestring[5]), int.Parse(messagestring[6]));
            MessageHash.Add(messagestring[0], message);
        }
        strings = textMessages.text.Split(';');
        for (int i = 0; i < strings.Length; i++)
        {
            string[] dialoguestring = strings[i].Split(',');
            DialogueClass dialogue= new DialogueClass(dialoguestring[1], dialoguestring[2], int.Parse(dialoguestring[3]), int.Parse(dialoguestring[4]));
            DialogueHash.Add(dialoguestring[0], dialogue);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
