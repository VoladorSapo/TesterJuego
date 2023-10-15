using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MessageAdder : MonoBehaviour
{

    public List<GameObject> TextConversations;
    [SerializeField] private GameObject MessageBoard;
    [SerializeField] private GameObject Options;
    int currentConversation;
    int rundown;
    public List<string> texts;
    List<List<MessageClass>> currentMessages; //Los mensajes que se van a añadir
    static List<List<MessageClass>> wholeMessages; //Todos los mensajes de la conversación
    [SerializeField] private GameObject MessagePrefab;
    [SerializeField] private GameObject ImagePrefab;
    [SerializeField] private GameObject OptionButtonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        rundown = 0;
        CloseBoard();
        print(currentMessages == null);
        if (wholeMessages == null || wholeMessages.Count == 0)
        {
            currentMessages = new List<List<MessageClass>>();

            wholeMessages = new List<List<MessageClass>>();
            for (int i = 0; i < TextConversations.Count; i++)
            {
                currentMessages.Add(new List<MessageClass>());

                wholeMessages.Add(new List<MessageClass>());
                TextConversations[i].GetComponent<CanvasGroup>().alpha = 0;
                TextConversations[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
        else
        {
            currentMessages = new List<List<MessageClass>>();

            for (int i = 0; i < TextConversations.Count; i++)
            {
                currentMessages.Add(new List<MessageClass>());

                foreach (MessageClass message in wholeMessages[i])
                {
                    AddMessage(message.text, message.side, i, message.isButton, 0);
                }
                TextConversations[i].GetComponent<CanvasGroup>().alpha = 0;
                TextConversations[i].GetComponent<CanvasGroup>().blocksRaycasts = false;

            }
        }

    }

    void AddMessage(string text, int side, int list, int isButton, int type)
    {
        print(type + " " + text);
        IEnumerator courutine;
        switch (type)
        {
            case 0:
                GameObject mensajeobject = Instantiate(MessagePrefab, TextConversations[list].transform.GetChild(0).transform.GetChild(0).transform);
                MensajeObjeto _mensaje = mensajeobject.GetComponent<MensajeObjeto>();
                 courutine = _mensaje.setMessage(text, side, isButton, 0);
                _mensaje.StartCoroutine(courutine);
                break;
            case 1:
                GameObject imagenobject = Instantiate(ImagePrefab, TextConversations[list].transform.GetChild(0).transform.GetChild(0).transform);
                ImagenObjeto _imagen = imagenobject.GetComponent<ImagenObjeto>();
                 courutine = _imagen.SetImage(text, side, isButton, 0);
                _imagen.StartCoroutine(courutine);
                break;
            case 2:
                Options.GetComponent<CanvasGroup>().alpha = 1;
              string[]  strings = text.Split('-');
                foreach (string item in strings)
                {

                }
                break;
            case 2:
                Options.GetComponent<CanvasGroup>().alpha = 1;
                text.Split('-');
                break;
        }

    }

    
    void AddMessageList(MessageClass[] messages, int conversation)
    {
        if (currentMessages[conversation].Count == 0)
        {
            currentMessages[conversation].AddRange(messages);
            wholeMessages[conversation].AddRange(messages);
            currentConversation = conversation;
            AddMessage(currentMessages[conversation][0].text, currentMessages[conversation][0].side, conversation,currentMessages[conversation][0].isButton, currentMessages[conversation][0].type);
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
            wholeMessages[conversation].AddRange(messages);
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

        foreach (MessageClass message in currentMessages[currentConversation].ToArray())
        {
            AddMessage(message.text, message.side, currentConversation, message.isButton, message.type);
            currentMessages[currentConversation].Remove(message);
            if(message.type == 2 || message.type == 3)
            {
                yield break;
            }
            // yield return new WaitForSeconds(WaitTime(message.text,message.time,message.waitTimeFixed));
            yield return new WaitForSeconds(2);
        }
        print("Bosa bosa");
        currentMessages[currentConversation].Clear();
    }
    public void OpenBoard()
    {
        MessageBoard.GetComponent<CanvasGroup>().alpha = 1;
        TextConversations[currentConversation].GetComponent<CanvasGroup>().alpha = 1;
        TextConversations[currentConversation].GetComponent<CanvasGroup>().blocksRaycasts = true;

        IEnumerator courutine = AddAllMessages();
        StartCoroutine(courutine);

    }
    public void ChangeConversation(int conver)
    {
        TextConversations[currentConversation].GetComponent<CanvasGroup>().alpha = 0;
        TextConversations[currentConversation].GetComponent<CanvasGroup>().blocksRaycasts = false;

        currentConversation = conver;
        MessageBoard.GetComponent<CanvasGroup>().alpha = 1;
        TextConversations[currentConversation].GetComponent<CanvasGroup>().alpha = 1;
        TextConversations[currentConversation].GetComponent<CanvasGroup>().blocksRaycasts = true;

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
        TextConversations[currentConversation].GetComponent<CanvasGroup>().blocksRaycasts = false;


    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddMessageList(new MessageClass[]{new MessageClass("Hola",0,0,0,false,0), new MessageClass("Caracola", 0, 0, 1, false, 0) , new MessageClass("Me duele el ano", 0, 0, 1, false, 0) }, 0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < texts.Count; i++)
            {
                AddMessage(texts[i], i % 2 == 0 ? 0 : 1, 0,0,0);

            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                SceneManager.LoadScene(4);
            }
            else
            {
                SceneManager.LoadScene(3);

            }
        }
    }
}
