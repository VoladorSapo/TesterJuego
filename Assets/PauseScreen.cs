using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseScreen : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        SetEvents();
        GetComponentInChildren<Button>().onClick.AddListener(delegate { PauseController.Instance.InvokeUnpause(); });

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        PauseController.Instance?.UnSetPausedEvents(Pause, Unpause);

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }

    public void Pause()
    {
        if(this!=null){
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        }else{
             PauseController.Instance?.UnSetPausedEvents(Pause, Unpause);
        }
    }

    public void Unpause()
    {
        if(this!=null){
        GetComponent<CanvasGroup>().alpha =0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        }else{
             PauseController.Instance?.UnSetPausedEvents(Pause, Unpause);
        }
    }

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }
}
