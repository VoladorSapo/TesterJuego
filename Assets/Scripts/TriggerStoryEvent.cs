using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStoryEvent : MonoBehaviour
{
    [SerializeField] string MessageKey;
    [SerializeField] int conversation;
    [SerializeField]   KeyCode key = KeyCode.None;

    [SerializeField] bool HardPause;
    [Header("0 Mensaje,1 DebugLog, 2 Conversacion")]
    [SerializeField] int SpawnPlace;//0 Mensaje,1 DebugLog, 2 Conversacion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEvent();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void TriggerEvent()
    {
        MessageClass[] messages = MessageAdder.Instance.GetMessageList(MessageKey);
        MessageAdder.Instance.AddMessageList(MessageAdder.Instance.GetMessageList(MessageKey), conversation);
        if (HardPause)
            MessageAdder.Instance.HardPause();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            print("abula");
        }
    }
}
