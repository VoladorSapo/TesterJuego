using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }
    public void Pause()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void Unpause()
    {
        GetComponent<CanvasGroup>().alpha =0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }
}
