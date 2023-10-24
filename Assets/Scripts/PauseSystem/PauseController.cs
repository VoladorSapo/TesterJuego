using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance;
    public event Action pauseEvent;
    public event Action unpauseEvent;
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
    public void SetPausedEvents(Action pause, Action unpause){
        pauseEvent+=pause;
        unpauseEvent+=unpause;
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            isPaused=!isPaused;

            if(isPaused)
            pauseEvent?.Invoke();
            else
            unpauseEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                pauseEvent?.Invoke();
            }
        }
    }
    public void InvokePause() //Lo he tenido que añadir para las pausas forzosas
    {
        pauseEvent?.Invoke();
        isPaused = true;
    }
}
