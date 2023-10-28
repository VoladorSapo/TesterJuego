using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaPistas : MonoBehaviour
{
    [SerializeField] int maxPistaAskTypes;
    [SerializeField] bool startonload;
    [SerializeField] bool giveTimed;
    [SerializeField] public bool startPuzzle;
    [SerializeField]public bool ended;
    [SerializeField] bool counting;
    [SerializeField] int count;
    [SerializeField] int puzzleCount;
    [SerializeField] List<float> waitSeconds = new List<float>();
    [SerializeField] float currentWait;
    [SerializeField] string key;
    public static SistemaPistas Instance;
    bool paused;
    // Start is called before the first frame update
    void Start()
    {
        currentWait = 0;
        count = 1;
        puzzleCount = 0;
        if (Instance == null)
        {
            Instance = this;
            if (startonload)
            {
                currentWait = 0;
                startPuzzle = true;
                counting = true;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            startPuzzle = true;
            counting = true;
        }
    }
    public void NextPuzzle(int i,string _key,float[] newwaits)
    {
        if(i > puzzleCount)
        {
            startPuzzle = true;
            counting = true;
            puzzleCount = i;
            key = _key;
            count = 1;
            waitSeconds.Clear();
            waitSeconds.AddRange(newwaits);
        }
    }
  public  void PedirPista()
    {
        List<MessageClass> allmessages = new List<MessageClass>();
        allmessages.Add(MessageAdder.Instance.GetMessageList("pedirpista_"+Random.Range(0,maxPistaAskTypes))[0]);
        MessageClass[] mensajes = MessageAdder.Instance.GetMessageList(key + "_" + count);
        if (mensajes.Length > 0)
        {
            allmessages.AddRange(mensajes);
            MessageAdder.Instance.AddMessageList(allmessages.ToArray(), 0);
            count++;
        }
        else
        {
            ended = true;
        }

    }
    void DarPista()
    {
            MessageClass[] mensajes = MessageAdder.Instance.GetMessageList(key+"_"+count);
            if (mensajes.Length > 0)
            {
                MessageAdder.Instance.AddMessageList(mensajes, 0);
            count++;
        }
        else
        {
            ended = true;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (counting && startPuzzle)
        {
            currentWait += Time.deltaTime;
            if (!ended && waitSeconds.Count > 0 && !paused && startPuzzle && currentWait > waitSeconds[0])
            {
                DarPista();
                waitSeconds.RemoveAt(0);
                currentWait = 0;
                counting = false;
            }
        }
    }
    public void Pause()
    {
        paused = true;
    }

    public void Unpause()
    {
        paused = false;
    }

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }
}
