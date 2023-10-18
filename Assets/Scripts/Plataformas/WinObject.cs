using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinObject : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SetEvents();
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
