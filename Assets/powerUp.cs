using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
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
