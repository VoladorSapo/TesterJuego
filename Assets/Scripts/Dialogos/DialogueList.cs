using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogueList : MonoBehaviour
{
   [SerializeField] TextAsset textDialogue;
   [SerializeField] TextAsset textMessages; 
    Dictionary<string,DialogueClass> DialogueHash;
   [SerializeField] Dictionary<string, MessageClass> MessageHash;
    public static DialogueList Instance;
    // Start is called before the first frame update
    void Start()
    {
      

    }
    private void Awake()
    {
        if (Instance == null)
        {
            DialogueHash = new Dictionary<string, DialogueClass>();
            MessageHash = new Dictionary<string, MessageClass>();
            Instance = this;
            string[] strings;
            if (textMessages != null)
            {
                 strings = textMessages.text.Split('\r');
                for (int i = 1; i < strings.Length; i++)
                {
                    string trimString = strings[i].Trim();
                    if (!string.IsNullOrWhiteSpace(trimString))
                    {
                        string[] messagestring = trimString.Split(';');
                        print(trimString);
                        MessageClass message = new MessageClass(messagestring[1], int.Parse(messagestring[2]), int.Parse(messagestring[3]), int.Parse(messagestring[4]), Convert.ToBoolean(messagestring[5]), messagestring[6]);
                        MessageHash.Add(messagestring[0], message);
                    }
                }
            }
            foreach (string item in MessageHash.Keys)
            {
                print(item);
            }
            if (textDialogue != null)
            {
                strings = textDialogue.text.Split('\r');
                print(strings.Length);

                for (int i = 0; i < strings.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(strings[i]))
                    {
                        string[] dialoguestring = strings[i].Split(';');
                        DialogueClass dialogue = new DialogueClass(dialoguestring[1], dialoguestring[2], int.Parse(dialoguestring[3]), int.Parse(dialoguestring[4]));
                        DialogueHash.Add(dialoguestring[0], dialogue);
                    }
                }
            }
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public DialogueClass getDialogue(string key)
    {
        if (DialogueHash.ContainsKey(key))
        {
            return DialogueHash[key];
        }
        else
        {
            print("Dialogue not found");
            return null;
        }
    }
    public MessageClass getMessage(string key)
    {
        print(MessageHash.Count);
        foreach (string item in MessageHash.Keys)
        {
            print(item);
        }
        if (MessageHash.ContainsKey(key))
        {
            return MessageHash[key];
        }
        else
        {
            print("Dialogue not found: " + key);
            return null;
        }
    }
}
