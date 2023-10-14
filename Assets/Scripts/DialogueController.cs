using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    TMP_Text text;
    TMP_TextInfo info;
    [SerializeField] bool escribiendo;
    [SerializeField] bool terminado;
    [SerializeField] string currenttext;
    [SerializeField] List<DialogueClass> CurrentConversation;
    [SerializeField] int currentChar;
    [SerializeField] int charpersec;
    CanvasGroup canvasgroup;
    [SerializeField] DialogueClass[] loadlist;
    List<TMP_LinkInfo> links;
    [SerializeField] TMP_SpriteAsset asset;
    // Start is called before the first frame update
    void Start()
    {
        print(Application.persistentDataPath);
        canvasgroup = GetComponent<CanvasGroup>();
        canvasgroup.alpha = 0;

        text = GetComponentInChildren<TMP_Text>();
        info = text.textInfo;
        CurrentConversation = new List<DialogueClass>();
    }
    public void LoadConversation()
    {
        CurrentConversation.AddRange(loadlist);
        canvasgroup.alpha = 1;
        StartText();
    }
    public void StartText()
    {
        if (CurrentConversation.Count > 0)
        {
            terminado = false;
            text.text = CurrentConversation[0].text;
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
            LoadConversation();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine("WriteText");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            text.spriteAsset = asset;
        }
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
        escribiendo = true;
        text.maxVisibleCharacters = 0;
        currentChar = 0;
        while (currentChar < info.characterCount)
        {
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

        }
    }
}
