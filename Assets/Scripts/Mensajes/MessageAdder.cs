using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageAdder : MonoBehaviour
{

    public List<GameObject> TextConversations;
    [SerializeField] private GameObject MessageBoard;
    int currentConversation;
    int rundown;
    public List<string> texts;
   List<List<MessageClass>> currentMessages;
    [SerializeField] private GameObject MessagePrefab;
        // Start is called before the first frame update
    void Start()
    {
        currentMessages = new List<List<MessageClass>>();
        rundown = 0;
        CloseBoard();
        for (int i = 0; i < TextConversations.Count; i++)
        {
            currentMessages.Add(new List<MessageClass>());
            TextConversations[i].GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    void AddMessage(string text, int type, int list)
    {
       GameObject mensajeobject = Instantiate(MessagePrefab, TextConversations[list].transform.GetChild(0).transform.GetChild(0).transform);
        MensajeObjeto _mensaje = mensajeobject.GetComponent<MensajeObjeto>();
        IEnumerator courutine = _mensaje.setMessage(text, type);
        _mensaje.StartCoroutine(courutine);

    }
    void AddMessageList(MessageClass[] messages,int conversation)
    {
        if ( currentMessages[conversation].Count == 0)
        {
            currentMessages[conversation].AddRange(messages);
            currentConversation = conversation;
            AddMessage(currentMessages[conversation][0].text, currentMessages[conversation][0].type, conversation);
            currentMessages[conversation].RemoveAt(0);
            if (MessageBoard.GetComponent<CanvasGroup>().alpha == 1 && TextConversations[conversation].GetComponent<CanvasGroup>().alpha == 1)
            {
                IEnumerator courutine = AddAllMessages();
                StartCoroutine(courutine);
            }
        }
        else
        {
            currentMessages[conversation].AddRange(messages);
            currentConversation = conversation;
            if (MessageBoard.GetComponent<CanvasGroup>().alpha == 1 && TextConversations[conversation].GetComponent<CanvasGroup>().alpha == 1)
            {
                IEnumerator courutine = AddAllMessages();
                StartCoroutine(courutine);
            }
        }
    }
    int WaitTime(string text, int time, bool waitfixed)
    {
        
        int wait = waitfixed ? time : text.ToCharArray().Length + time;
        return wait;
    }
    IEnumerator AddAllMessages()
    {
        yield return new WaitForSeconds(2);

        foreach (MessageClass message in currentMessages[currentConversation])
        {
            AddMessage(message.text, message.type, currentConversation);
            // yield return new WaitForSeconds(WaitTime(message.text,message.time,message.waitTimeFixed));
            yield return new WaitForSeconds(2);
        }
        currentMessages[currentConversation].Clear();
    }
    public void OpenBoard()
    {
        MessageBoard.GetComponent<CanvasGroup>().alpha = 1;
        TextConversations[currentConversation].GetComponent<CanvasGroup>().alpha = 1;
        IEnumerator courutine = AddAllMessages();
        StartCoroutine(courutine);

    }
    public void ChangeConversation(int conver)
    {
        TextConversations[currentConversation].GetComponent<CanvasGroup>().alpha = 0;

        currentConversation = conver;
        MessageBoard.GetComponent<CanvasGroup>().alpha = 1;
        TextConversations[currentConversation].GetComponent<CanvasGroup>().alpha = 1;
        if (currentMessages[currentConversation].Count > 0)
        {
            IEnumerator courutine = AddAllMessages();
            StartCoroutine(courutine);
        }
    }
    public void CloseBoard()
    {
        MessageBoard.GetComponent<CanvasGroup>().alpha = 0;
        TextConversations[currentConversation].GetComponent<CanvasGroup>().alpha = 0;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddMessage(texts[rundown], rundown % 2 == 0 ? 0 : 1, 0);
            rundown++;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < texts.Count; i++)
            {
                AddMessage(texts[i], i % 2 == 0 ? 0 : 1, 0);

            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddMessageList(new MessageClass[] { new MessageClass(0, 0, "Montate en mi motora", false), new MessageClass(1, 0, "Desayuna con huevo", false) , new MessageClass(0, 0, "Toma Mango", false) }, 0);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddMessageList(new MessageClass[] { new MessageClass(0, 0, "*Se quita la ropa*", false), new MessageClass(1, 0, "*Le nalgea*", false) }, 1);
        }
    }
}
