using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformasSpriteController : MonoBehaviour
{
    Animator anim;
    public void DoubleJump()
    {
        anim.SetBool("doubleJump", false);

    }
    public void EndDeath()
    {
        anim.SetBool("Die", false);
        transform.parent.GetComponent<PlatormerPlayerController>().Respawn();

    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
