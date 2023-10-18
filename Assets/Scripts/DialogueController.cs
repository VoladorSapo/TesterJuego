using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    public Action endConversation;
    public static DialogueController Instance;
    TMP_Text text;
    TMP_TextInfo info;
    [SerializeField] bool escribiendo;
    [SerializeField] bool terminado;
    [SerializeField] string currenttext;
    [SerializeField] List<DialogueClass> CurrentConversation;
    [SerializeField] int currentChar;
    [SerializeField] int charpersec;
    [SerializeField] Animator character;
    [SerializeField] TMP_Text nombre;
    CanvasGroup canvasgroup;
    [SerializeField] DialogueClass[] loadlist;
    List<TMP_LinkInfo> links;
    [SerializeField] TMP_SpriteAsset[] assets;
    // Start is called before the first frame update

    void Awake(){
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        print(Application.persistentDataPath);
        canvasgroup = GetComponent<CanvasGroup>();
        canvasgroup.alpha = 0;

        text = GetComponentInChildren<TMP_Text>();
        nombre = GetComponentsInChildren<TMP_Text>()[1];
        character = GetComponentsInChildren<Animator>()[0];

        info = text.textInfo;
        CurrentConversation = new List<DialogueClass>();
    }
    public void getConversation(string basekey)
    {
        List<DialogueClass> dialogs = new List<DialogueClass>();
        int i = 0;
        DialogueClass dialog = DialogueList.Instance.getDialogue(basekey + "_" + i);
        while(dialog != null)
        {
            i++;
            dialogs.Add(dialog);
            dialog = DialogueList.Instance.getDialogue(basekey + "_" + i);
        }
        print(dialogs.Count);
        StartConversation(dialogs.ToArray());
    }
    public void StartConversation(DialogueClass[] dialogs)
    {
        CurrentConversation.AddRange(dialogs);
        canvasgroup.alpha = 1;
        StartText();
    }
    public void StartText()
    {
        if (CurrentConversation.Count > 0)
        {
            terminado = false;
            text.text = CurrentConversation[0].text;
            character.SetInteger("Character", CurrentConversation[0].Character);
            character.SetInteger("Anim", CurrentConversation[0].anim);
            nombre.text = CurrentConversation[0].nombre;
            CurrentConversation.RemoveAt(0);
            info = text.textInfo;
            GetLinks();
            StartCoroutine("WriteText");
        }
        else
        {
            EndDialogue();
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            getConversation("dialog");
                }/*
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine("WriteText");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            text.spriteAsset = assets[0];
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            if (terminado)
            {
                StartText();
            }
            else
            {
                currentChar = text.maxVisibleCharacters = info.characterCount;
                terminado = true;
            }
        }
    }
    IEnumerator WriteText()
    {
        print("cuidap");
        escribiendo = true;
        text.maxVisibleCharacters = 0;
        currentChar = 0;
        while (currentChar < info.characterCount)
        {
            print("cuip");

            text.maxVisibleCharacters++;
            currentChar++;
            checkLink();
            yield return delay();
        }
        terminado = true;

    }
    void EndDialogue()
    {
        canvasgroup.alpha = 0;
        endConversation?.Invoke();
    }
    WaitForSeconds delay()
    {
        return new WaitForSeconds(1 / charpersec);
    }
    void GetLinks()
    {
        var Linkcount = text.textInfo.linkCount;
        links = new List<TMP_LinkInfo>();
        links.AddRange(info.linkInfo);
        print(links.Count);
        foreach (TMP_LinkInfo item in links)
        {
            print(item.GetLinkID() + " " + item.linkTextfirstCharacterIndex);
        }
    }

    void checkLink()
    {
        if (links.Count > 0)
        {
            for (int i = 0; i < links.Count; i++)
            {
                if (currentChar > links[0].linkTextfirstCharacterIndex)
                {

                    print(links[0].linkTextfirstCharacterIndex);
                    print(links[0].GetLinkID());
                    links.RemoveAt(0);
                    i--;
                }
                else
                {
                    break;
                }
            }
        }
    }
    void LinkFunction(string linktext)
    {
        string[] linkarray = linktext.Split('-');
        switch (linkarray[0])
        {
            case "TextAsset":
                text.spriteAsset = assets[int.Parse(linkarray[1])];
                break;
        }
    }
   public void setEvent(Action end)
    {
        endConversation += end;
    }
   public void unSetEvent(Action end)
    {
        endConversation -= end;
    }
}
