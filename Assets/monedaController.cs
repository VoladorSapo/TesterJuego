using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monedaController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        SetEvents();
    }
    private void OnDestroy()
    {
        PauseController.Instance?.UnSetPausedEvents(Pause, Unpause);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pause()
    {
        anim.speed = 0;
    }

    public void Unpause()
    {
        anim.speed = 1;
    }

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }
}
