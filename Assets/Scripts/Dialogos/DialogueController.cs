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
    public event Action endConversation;
    public event Action startConversation;
    public static DialogueController Instance;
 [SerializeField]   TMP_Text text;
    TMP_TextInfo info;
    [SerializeField] bool escribiendo;
    [SerializeField] bool terminado;
    [SerializeField] string currenttext;
    [SerializeField] List<DialogueClass> CurrentConversation;
    [SerializeField] int currentChar;
    [SerializeField] int charpersec;
    [SerializeField] bool isIntro;
    [SerializeField] Animator character;
    [SerializeField] TMP_Text nombre;
    CanvasGroup canvasgroup;
    [SerializeField] DialogueClass[] loadlist;
    List<TMP_LinkInfo> links;
    [SerializeField] TMP_SpriteAsset[] assets;
    // Start is called before the first frame update

    void Awake(){
        if (!isIntro)
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
    void Start()
    {
        if (!isIntro)
        {
            print(Application.persistentDataPath);
            canvasgroup = GetComponent<CanvasGroup>();
            canvasgroup.alpha = 0;

            text = GetComponentInChildren<TMP_Text>();
            nombre = GetComponentsInChildren<TMP_Text>()[1];
            character = GetComponentsInChildren<Animator>()[0];

            info = text.textInfo;
        }
        else
        {
            info = text.textInfo;

            canvasgroup = character.gameObject.GetComponent<CanvasGroup>();
        }
        CurrentConversation = new List<DialogueClass>();

    }

    public void setEventConversations(Action start, Action end){
        startConversation+=start;
        endConversation+=end;
    }
    public void unsetEventConverstaions(Action start, Action end){
        startConversation-=start;
        endConversation-=end;
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
        startConversation?.Invoke();
        print(dialogs.Length);
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
            if(SceneManager.GetActiveScene().name == "Intro" && CurrentConversation.Count == 1)
            {
                print("diad");
                EventGallery.Instance.IntroGlitch();
            }
            else
            {
                print(SceneManager.GetActiveScene().name);
                print(CurrentConversation.Count);
                print("elsee");
            }
            if (!isIntro)
            {
                nombre.text = CurrentConversation[0].nombre;

            }

            if (nombre.text == "None")
            {
                nombre.gameObject.GetComponentInParent<CanvasGroup>().alpha = 0;
            }
            else
            {
                nombre.gameObject.GetComponentInParent<CanvasGroup>().alpha = 0;
            }
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
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    getConversation("dialog");
        //        }
        /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine("WriteText");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            text.spriteAsset = assets[0];
        }*/
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Return))
        {
            if (terminado)
            {
                StartText();
                checkLink();

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
        text.maxVisibleCharacters = int.MaxValue;
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
                    LinkFunction(links[0].GetLinkID());
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
        print("ayuda");
        switch (linkarray[0])
        {
            case "TextAsset":
                text.spriteAsset = assets[int.Parse(linkarray[1])];
                break;
            case "Toshida":FindObjectOfType<GlobalWarpPoint>().DoTransition();print("papu"); break;
        }
    }
    public void setEndConversation(Action end)
    {
        endConversation += end;
    }
   public void unsetEndConversation(Action end)
    {
        endConversation -= end;
    }
}
