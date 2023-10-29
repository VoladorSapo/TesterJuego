using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogueList : MonoBehaviour
{
   [SerializeField] TextAsset textDialogue;
   [SerializeField] TextAsset textMessages;
    [SerializeField] TextAsset textLog;
    Dictionary<string,DialogueClass> DialogueHash;
   [SerializeField] Dictionary<string, MessageClass> MessageHash;
    Dictionary<string, string> LogHash;
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
            LogHash = new Dictionary<string, string>();
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
                        MessageClass message = new MessageClass(messagestring[1], int.Parse(messagestring[2]), int.Parse(messagestring[3]), int.Parse(messagestring[4]), Convert.ToBoolean(messagestring[5]), messagestring[6]);
                        MessageHash.Add(messagestring[0], message);
                    }
                }
            }
            if (textDialogue != null)
            {
                strings = textDialogue.text.Split('\r');

                for (int i = 1; i < strings.Length; i++)
                {
                    string trimString = strings[i].Trim();

                    if (!string.IsNullOrWhiteSpace(trimString))
                    {

                        string[] dialoguestring = trimString.Split(';');
                        DialogueClass dialogue = new DialogueClass(dialoguestring[1], dialoguestring[2], int.Parse(dialoguestring[3]), int.Parse(dialoguestring[4]));
                        DialogueHash.Add(dialoguestring[0], dialogue);
                    }
                }
            }
            if (textLog != null)
            {
                strings = textLog.text.Split('\r');
                print(strings.Length);

                for (int i =1; i < strings.Length; i++)
                {
                    string trimString = strings[i].Trim();
                    if (!string.IsNullOrWhiteSpace(trimString))
                    {
                        string[] dialoguestring = trimString.Split(';');
                        print(dialoguestring[0]);
                        print(dialoguestring[1]);
                        LogHash.Add(dialoguestring[0], dialoguestring[1]);
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
    public string getLog(string key)
    {
        if (LogHash.ContainsKey(key))
        {
            return LogHash[key];
        }
        else
        {
            print("Log not found: " + key);
            return null;
        }
    }
}
