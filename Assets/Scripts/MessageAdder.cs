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
    List<MessageClass> currentMessages;
    [SerializeField] private GameObject MessagePrefab;
        // Start is called before the first frame update
    void Start()
    {
        rundown = 0;
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
        currentMessages.Clear();
        currentMessages.AddRange(messages);
        currentConversation = conversation;
        AddMessage(currentMessages[0].text, currentMessages[0].type, conversation);
        currentMessages.RemoveAt(0);
        if (MessageBoard.activeSelf && TextConversations[conversation].activeSelf)
        {
            IEnumerator courutine = AddAllMessages();
            StartCoroutine(courutine);
        }
    }
    int WaitTime(string text, int time, bool waitfixed)
    {
        
        int wait = waitfixed ? time : text.ToCharArray().Length + time;
        return wait;
    }
    IEnumerator AddAllMessages()
    {
        foreach (MessageClass message in currentMessages)
        {
            AddMessage(message.text, message.type, currentConversation);
            yield return new WaitForSeconds(WaitTime(message.text,message.time,message.waitTimeFixed));

        }
    }
    public void OpenBoard()
    {
        MessageBoard.SetActive(true);
        TextConversations[currentConversation].SetActive(true);
        AddAllMessages();

    }
    public void CloseBoard()
    {
        MessageBoard.SetActive(false);
        TextConversations[currentConversation].SetActive(false);

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
    }
}
