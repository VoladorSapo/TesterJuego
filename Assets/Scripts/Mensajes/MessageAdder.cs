using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MessageAdder : MonoBehaviour
{
    [SerializeField] bool[] ShowConversations;
    public List<GameObject> TextConversations;
    [SerializeField] GameObject ButtonList;
    [SerializeField] GameObject conversationAdder;
   public static MessageAdder Instance;
    [SerializeField] private GameObject MessageBoard;
    [SerializeField] GameObject conversationPrefab;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] private GameObject Options;
    int currentConversation;
    int rundown;
    public List<string> texts;
    List<List<MessageClass>> currentMessages; //Los mensajes que se van a a�adir
    static List<List<MessageClass>> wholeMessages; //Todos los mensajes de la conversaci�n
    [SerializeField] private GameObject MessagePrefab;
    [SerializeField] private GameObject ImagePrefab;
    [SerializeField] private GameObject OptionButtonPrefab;
    [SerializeField] public Button OpenButton;
    [SerializeField] string[] Telefonos;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] bool HardPaused;
    [SerializeField] bool Writting;
   [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            anim.SetBool("HasMessage", false);
            SetEvents();
            if (SceneManager.GetActiveScene().name == "Nivel 1" || SceneManager.GetActiveScene().name == "Menu Plataformas")
                OpenButton.gameObject.SetActive(false);
            rundown = 0;
            CloseBoard();
            print(currentMessages == null);
            conversationAdder.GetComponent<CanvasGroup>().alpha = 0;
            conversationAdder.GetComponent<CanvasGroup>().interactable = false;
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
                        AddMessage(message.text, message.side, i, message.isButton, message.type);
                    }
                    TextConversations[i].GetComponent<CanvasGroup>().alpha = 0;
                    TextConversations[i].GetComponent<CanvasGroup>().blocksRaycasts = false;

                }
            }
            conversationAdder.GetComponent<CanvasGroup>().alpha = 0;
            conversationAdder.GetComponent<CanvasGroup>().blocksRaycasts = false;
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void AddMessage(string text, int side, int list, string isButton, int type)
    {
        print(type + " " + text);
        IEnumerator courutine;
        string[] strings = new string[0];
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
                strings = text.Split('-');
                for (int i = 1; i < strings.Length; i++)
                {
                    Button newbutton = Instantiate(OptionButtonPrefab, Options.transform).GetComponent<Button>();
                    newbutton.GetComponentInChildren<TMP_Text>().text = strings[i];
                    print(newbutton.name + " " + i);
                    string a = strings[0] + "_" + i;
                    newbutton.onClick.AddListener(delegate { ReturnWithExtraMessages(a); });
                }
                break;
            case 3:
                Options.GetComponent<CanvasGroup>().alpha = 1;
                text.Split('-');
                strings = text.Split('-');
                for (int i = 0; i < strings.Length; i++)
                {
                    Button newbutton = Instantiate(OptionButtonPrefab, Options.transform).GetComponent<Button>();
                    newbutton.GetComponentInChildren<TMP_Text>().text = strings[i];
                    newbutton.onClick.AddListener(delegate { ReturntoMessages(); });
                }
                break;
        }
        anim.SetBool("HasMessage", hasMessage());

    }
    void ReturntoMessages()
    {
        foreach (Transform child in Options.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        print("return");
        Options.GetComponent<CanvasGroup>().alpha = 0;
        IEnumerator couritine = AddAllMessages(0);
        StartCoroutine(couritine);
    }

    void ReturnWithExtraMessages(string action)//Porque no puedes darle a un boton un ienumerator
    {
        print("Socorro");
        MessageClass[] messages = GetMessageList(action);
        currentMessages[currentConversation].InsertRange(0, messages);
        Options.GetComponent<CanvasGroup>().alpha = 0;
        IEnumerator couritine = AddAllMessages(0);
        foreach (Transform child in Options.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        StartCoroutine(couritine);
    }
    private void OnDestroy()
    {
        PauseController.Instance?.UnSetPausedEvents(Pause, Unpause);
    }
    public MessageClass[] GetMessageList(string key)
    {

        List<MessageClass> messages = new List<MessageClass>();
        int i = 0;
        MessageClass dialog = DialogueList.Instance.getMessage(key + "_" + i);
        while (dialog != null)
        {
            print("waka");
            i++;
            messages.Add(dialog);
            dialog = DialogueList.Instance.getMessage(key + "_" + i);
        }
        return messages.ToArray();
    }
    public void AddMessageList(MessageClass[] messages, int conversation)
    {
        if (currentMessages[conversation].Count > 0)
        {
            ForceAddMessages(conversation);
        }
        ShowConversations[conversation] = true;
        ButtonList.transform.GetChild(conversation).gameObject.SetActive(true);
        currentMessages[conversation].AddRange(messages);
        wholeMessages[conversation].AddRange(messages);
        currentConversation = conversation;
        AddMessage(currentMessages[conversation][0].text, currentMessages[conversation][0].side, conversation, currentMessages[conversation][0].isButton, currentMessages[conversation][0].type);
        currentMessages[conversation].RemoveAt(0);
        if (MessageBoard.GetComponent<CanvasGroup>().alpha == 1 && TextConversations[conversation].GetComponent<CanvasGroup>().alpha == 1)
        {
            print("hory shit");
            IEnumerator courutine = AddAllMessages(2);
            StartCoroutine(courutine);
        }
        else
        {
            print("heeek");
            if (this.transform != null)
            {
                AudioManager.Instance.PlaySound("Notif 2", false, this.transform.position, false);
            }
        }
        anim.SetBool("HasMessage", hasMessage());

    }
    int WaitTime(string text, int time, bool waitfixed)
    {
        return 3;
        if (waitfixed)
            return time;
        int wait = text.ToCharArray().Length / 15;
        return wait;
    }
    IEnumerator AddAllMessages(int firstWait)
    {
        if (currentMessages[currentConversation].Count > 0)
        {
            StartWrittin();
            bool noStop = true;
            print(currentMessages[currentConversation].Count);
            yield return new WaitForSeconds(firstWait);
            foreach (MessageClass message in currentMessages[currentConversation].ToArray())
            {
                print("socrro");
                AddMessage(message.text, message.side, currentConversation, message.isButton, message.type);
                currentMessages[currentConversation].Remove(message);
                if (message.type == 2 || message.type == 3)
                {
                    noStop = false;
                    break;
                }
                yield return new WaitForSeconds(WaitTime(message.text, message.time, message.waitTimeFixed));
            }
            if (noStop)
            {
                currentMessages[currentConversation].Clear();
                anim.SetBool("HasMessage", hasMessage());
                EndWrittin();
            }
        }
    }
    void ForceAddMessages(int conversation)
    {
        foreach (MessageClass message in currentMessages[conversation].ToArray())
        {
            if (message.type == 2 || message.type == 3)
            {
                foreach (MessageClass optionmessage in GetMessageList(message.text.Split('-')[0]+"_0"))
                {
                    AddMessage(optionmessage.text, optionmessage.side, currentConversation, optionmessage.isButton, optionmessage.type);

                }
            }
            else {
                AddMessage(message.text, message.side, currentConversation, message.isButton, message.type);
                currentMessages[currentConversation].Remove(message);
            }
            currentMessages[currentConversation].Remove(message);

        }
        bool b = hasMessage();
        anim.SetBool("HasMessage", b);
    }
    public bool hasMessage()
    {

        print("Has mesasg");
        print(currentMessages[0].Count);
        for (int i = 0; i < currentMessages.Count; i++)
        {
            if(currentMessages[i].Count > 0)
            {
                print("true");
                return true;
            }
        }
        print("false");
        return false;
    }
    public void SwitchBoard()
    {
        if (MessageBoard.GetComponent<CanvasGroup>().alpha == 1)
        {
            CloseBoard();
        }
        else
        {
            OpenBoard();
        }
    }
    public void OpenBoard()
    {
        anim.SetBool("Open", true);
        print("ooooooo");
        MessageBoard.GetComponent<CanvasGroup>().alpha = 1;
        TextConversations[currentConversation].GetComponent<CanvasGroup>().alpha = 1;
        TextConversations[currentConversation].GetComponent<CanvasGroup>().blocksRaycasts = true;
        print("rjnfjsnf");
        if (currentConversation == 0 && currentMessages[currentConversation].Count <= 0)
        {
            CheckPuzzlePista();
        }
        for (int i = 0; i < TextConversations.Count-1; i++)
        {
            if (!ShowConversations[i])
            {
                ButtonList.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        IEnumerator courutine = AddAllMessages(2);
        StartCoroutine(courutine);

    }
    public void showOpenButton(bool a)
    {
        OpenButton.gameObject.SetActive(a);
    }
    public void OpenButtonSetInteract(bool a)
    {
        OpenButton.interactable = a;
    }
    public void ChangeConversation(int conver)
    {
        if (currentConversation != conver)
        {
            TextConversations[currentConversation].GetComponent<CanvasGroup>().alpha = 0;
            TextConversations[currentConversation].GetComponent<CanvasGroup>().blocksRaycasts = false;

            currentConversation = conver;
            MessageBoard.GetComponent<CanvasGroup>().alpha = 1;
            TextConversations[currentConversation].GetComponent<CanvasGroup>().alpha = 1;
            TextConversations[currentConversation].GetComponent<CanvasGroup>().blocksRaycasts = true;

            if (currentConversation != 0)
            {
                foreach (Transform child in Options.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            if (currentMessages[currentConversation].Count > 0)
            {
                print("oooo noooo");
                IEnumerator courutine = AddAllMessages(2);
                StartCoroutine(courutine);
            }
            else
            {
                if (currentConversation == 0)
                {
                    CheckPuzzlePista();
                }
            }
        }
        else
        {
            print("socoroo");
        }
    }
    void CheckPuzzlePista()
    {
        print("wacamalo");
        if(SistemaPistas.Instance != null && SistemaPistas.Instance.startPuzzle && !SistemaPistas.Instance.ended)
        {
            Button newbutton = Instantiate(OptionButtonPrefab, Options.transform).GetComponent<Button>();
            Options.GetComponent<CanvasGroup>().alpha = 1;

            newbutton.GetComponentInChildren<TMP_Text>().text = "Pedir ayuda";
            newbutton.onClick.AddListener(delegate { SistemaPistas.Instance.PedirPista(); Options.GetComponent<CanvasGroup>().alpha = 0; foreach (Transform child in Options.transform)
                {
                    GameObject.Destroy(child.gameObject);
                   

                }
            });
        }
    }
    public void CloseBoard()
    {
        anim.SetBool("Open", false);
        print(name);
        MessageBoard.GetComponent<CanvasGroup>().alpha = 0;
            foreach (Transform child in Options.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        Options.GetComponent<CanvasGroup>().alpha = 0;
    TextConversations[currentConversation].GetComponent<CanvasGroup>().alpha = 0;
        TextConversations[currentConversation].GetComponent<CanvasGroup>().blocksRaycasts = false;


    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //public void LoadScene(int Scene)
    //{
    //    SceneManager.LoadScene(Scene);
    //}
    public void AddButtonClick(Button _button, string code)
    {
        string[] codes = code.Split('-');
        switch (codes[0])
        {
            case "Scene":
                _button.onClick.AddListener(delegate { FindObjectOfType<GlobalWarpPoint>().DoTransition(); CloseBoard();PauseController.Instance.InvokeUnpause(); });
                break;
            case "SceneZelda":
                _button.onClick.AddListener(delegate {
             GameObject wap = GameObject.Find("WarpZelda");
                    if (wap)
                    {
                        wap.GetComponent<GlobalWarpPoint>().DoTransition();
                        CloseBoard();
                        PauseController.Instance.InvokeUnpause();
                    } });

                break;
            default:
                break;
        }
    }
    void StartWrittin()
    {
        OpenButton.interactable = false;
        foreach (Button button in ButtonList.GetComponentsInChildren<Button>())
        {
            if (button.transform.GetSiblingIndex() != currentConversation)
            {
                button.interactable = false;
            }
        }
        PauseController.Instance.hardPause();
    }
    void EndWrittin()
    {
        OpenButton.interactable = true;
        foreach (Button button in ButtonList.GetComponentsInChildren<Button>())
        {
            button.interactable = true;
        }
        PauseController.Instance.unHardPause();
    }
    public void LinkCount(TMP_Text text)
    {
        var Linkcount = text.textInfo.linkCount;
        print(Linkcount);
        foreach (TMP_LinkInfo item in text.textInfo.linkInfo)
        {
            LinkFunction(item.GetLinkID());
        }
    }
    void LinkFunction(string linktext)
    {
        string[] linkarray = linktext.Split('-');
        switch (linkarray[0])
        {
            case "LoadScene":
                SceneManager.LoadScene(linkarray[1]);
                break;
            case "Pause":
                PauseController.Instance.InvokePause();
                OpenButton.gameObject.SetActive(false);
                break;
            case "UnHardPause":
                HardPaused = false;
                OpenButton.gameObject.SetActive(false);
                break;

        }
    }
    public void OpenAdder()
    {
        conversationAdder.GetComponent<CanvasGroup>().alpha = conversationAdder.GetComponent<CanvasGroup>().alpha == 0 ? 1 : 0;
        conversationAdder.GetComponent<CanvasGroup>().blocksRaycasts = conversationAdder.GetComponent<CanvasGroup>().alpha == 0 ? false : true;
        conversationAdder.GetComponent<CanvasGroup>().interactable = conversationAdder.GetComponent<CanvasGroup>().blocksRaycasts;
    }
    public void AddConversation()
    {
        print(inputField.text);
        switch (inputField.text)
        {
            case "6223143":
                if (SceneManager.GetActiveScene().name == "ConversacionIntermediaTelefono")
                {
                    OpenAdder();
                    ChangeConversation(2);
                    MessageClass[] messages = MessageAdder.Instance.GetMessageList("julia");
                    MessageAdder.Instance.AddMessageList(messages, 2);
                }
                inputField.text = "Teléfono...";
                break;
            default:
                OpenAdder();
                inputField.text = "Teléfono...";
                break;
        }
    }
    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }
    public void Unpause()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CloseBoard();
       // OpenButton.gameObject.SetActive(false);
    }
    public void HardPause()
    {
        print("diablo");
        //OpenBoard();
        //OpenButton.gameObject.SetActive(false);
        PauseController.Instance.hardPause();
    }
    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //OpenButton.gameObject.SetActive(true);

    }
}
