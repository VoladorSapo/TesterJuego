using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStoryEvent : MonoBehaviour
{
    [SerializeField] string MessageKey;
    [SerializeField] int conversation;
    [SerializeField] KeyCode key = KeyCode.None;
    [SerializeField] bool onLoad;
    [SerializeField] public string LoadStringEvent;
    [SerializeField] int carNarrativeMoment = -1;
    public string[] summonkeys;
    [SerializeField] bool HardPause;
    [SerializeField] float waitTime;
    [SerializeField] bool waitsec;
    [Header("0 Mensaje,1 DebugLog, 2 Conversacion")]
    [SerializeField] int SpawnPlace;//0 Mensaje,1 DebugLog, 2 Conversacion
    [Header("Solo pa debug")]
    public bool HasTriggered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((carNarrativeMoment == -1 || carNarrativeMoment == 0 || carNarrativeMoment == SceneManagement.Instance.narrativeParts.CarNarrative) && (string.IsNullOrWhiteSpace(LoadStringEvent) || LoadStringEvent == SceneManagement.Instance.actionName))
        {
            TriggerEvent();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!waitsec)
        {
            if (onLoad && (carNarrativeMoment == -1 || carNarrativeMoment == SceneManagement.Instance.narrativeParts.CarNarrative) && (string.IsNullOrWhiteSpace(LoadStringEvent) || LoadStringEvent == SceneManagement.Instance.actionName))
            {
                TriggerEvent();
            }
        }
        else
        {
            StartCoroutine(waitSeconds());
        }
    }
    IEnumerator waitSeconds()
    {
        yield return new WaitForSeconds(waitTime);
        TriggerEvent();
    }
    public void TriggerEvent()
    {
        if (!HasTriggered)
        {
            switch (SpawnPlace)
            {
                case 0:
                    MessageClass[] messages = MessageAdder.Instance.GetMessageList(MessageKey);
                    MessageAdder.Instance.AddMessageList(MessageAdder.Instance.GetMessageList(MessageKey), conversation);
                    MessageAdder.Instance.showOpenButton(true);
                    HasTriggered = true;
                    if (HardPause)
                        MessageAdder.Instance.HardPause();
                    break;
                case 1:
                    StartCoroutine(  DebugLogController.Instance.AddLogs(DebugLogController.Instance.GetLogs(MessageKey)));
                    DebugLogController.Instance.checkKey(MessageKey);
                    HasTriggered = true;
                    break;
            }
        }
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
