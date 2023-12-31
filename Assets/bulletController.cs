using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    [SerializeField] LayerMask layersHit;
    [ SerializeField ]  Vector2 saveSpeed;
    Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((layersHit.value & 1 << collision.gameObject.layer) > 0) && collision.gameObject.transform != transform.parent && collision.gameObject.transform.parent != transform.parent)
        {
            AudioManager.Instance.PlaySound("Bullet", false, transform.position, false);
            anim.SetBool("Boom", true);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
    public void DestroySelf()
    {
        PauseController.Instance?.UnSetPausedEvents(Pause, Unpause);
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren < Animator>();
        SetEvents();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pause()
    {
            saveSpeed = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        anim.speed = 0;
    }

    public void Unpause()
    {
        anim.speed = 1;
        GetComponent<Rigidbody2D>().velocity = saveSpeed;
    }

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }
}
