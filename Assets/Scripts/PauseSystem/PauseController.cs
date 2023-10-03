using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance;
    public UnityEvent pauseEvent;
    public UnityEvent unpauseEvent;
    bool isPaused=false;

    void Awake(){
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetPausedEvents(UnityAction pause, UnityAction unpause){
        pauseEvent.AddListener(pause);
        unpauseEvent.AddListener(unpause);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            isPaused=!isPaused;

            if(isPaused)
            pauseEvent.Invoke();
            else
            unpauseEvent.Invoke();
        }
    }
}
