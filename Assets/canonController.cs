using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonController : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] float WaitTime;
[SerializeField]    float currentWait;
    [SerializeField] float speed;
    [SerializeField] float bulletAcceleration;
 [SerializeField]   int Dir;
    Animator anim;
    bool paused;
    // Start is called before the first frame update
    void Start()
    {
        currentWait = WaitTime;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (currentWait <= 0)
            {

                anim.SetBool("Shooting", true);
            }
            else
            {
                currentWait -= Time.deltaTime;
            }
        }
    }
    public void Shoot()
    {
        AudioManager.Instance.PlaySound("Shoot", false, transform.position, false);

        GameObject _bala = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 90 * Dir), transform);
        Vector2 Vectorspeed = (Dir % 2 == 0) ? new Vector2(1, 0) : new Vector2(0, 1);
        Vectorspeed *= speed * (Dir < 2 ? -1 : 1);
        _bala.GetComponent<Rigidbody2D>().velocity = Vectorspeed;
        _bala.GetComponent<Rigidbody2D>().gravityScale = bulletAcceleration;
    }
  public  void ResetAnim()
    {
        currentWait = WaitTime;

        anim.SetBool("Shooting", false);

    }
    public void Pause()
    {
        anim.speed = 0;
        paused = true;
    }

    public void Unpause()
    {
        anim.speed = 1;
        paused = false;
    }

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }
}
