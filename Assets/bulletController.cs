using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
   [SerializeField] LayerMask layersHit;
    Vector2 saveSpeed;
    Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((layersHit.value & 1 << collision.gameObject.layer) > 0) && collision.gameObject.transform != transform.parent && collision.gameObject.transform.parent != transform.parent)
        {
            AudioManager.Instance.PlaySound("Bullet", false, transform.position, false);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent < Animator>();
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
