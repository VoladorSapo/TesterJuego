using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance;
    public event Action pauseEvent;
    public event Action unpauseEvent;
    [SerializeField] bool isPaused=false;
    bool hardPaused = false;
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
        
        isPaused=false;
        
    }

    void Start(){
        SceneManagement.Instance.killMouse=false;
    }
    
    public void SetPausedEvents(Action pause, Action unpause){
        pauseEvent+=pause;
        unpauseEvent+=unpause;
    }
    public void UnSetPausedEvents(Action pause, Action unpause)
    {
        pauseEvent -= pause;
        unpauseEvent -= unpause;
    }
    void Update(){
        
        //if(Input.GetKeyDown(KeyCode.P)){
        //    isPaused=!isPaused;

        //    if(isPaused)
        //    pauseEvent?.Invoke();
        //    else
        //    unpauseEvent?.Invoke();
        //}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!hardPaused)
            {
                isPaused = !isPaused;

                if (isPaused){
                    SceneManagement.Instance.killMouse=false;
                    pauseEvent?.Invoke();
                }
                else if(!isPaused){
                    SceneManagement.Instance.killMouse=true;
                    unpauseEvent?.Invoke();
                }
            }
        }
    }
    public void hardPause()
    {
        hardPaused = true;
       PauseScreen pause =  FindObjectOfType<PauseScreen>();
        if(pause != null)
        {
            pause.gameObject.GetComponentInChildren<Button>().interactable = false;
        }
        InvokePause();
    }
  public void  unHardPause()
    {
        hardPaused = false;
        PauseScreen pause = FindObjectOfType<PauseScreen>();
        if (pause != null)
        {
            pause.gameObject.GetComponentInChildren<Button>().interactable = true;
        }
    }
    public void InvokePause() //Lo he tenido que a�adir para las pausas forzosas
    {
        pauseEvent?.Invoke();
        isPaused = true;
    }
    public void InvokeUnpause()
    {
        unpauseEvent?.Invoke();
        isPaused = false;
    }
}
