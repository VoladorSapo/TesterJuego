using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonController : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] float WaitTime;
    float currentWait;
    [SerializeField] float speed;
 [SerializeField]   int Dir;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        currentWait = WaitTime;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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
    public void Shoot()
    {
        GameObject _bala = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 90 * Dir), transform);
        Vector2 Vectorspeed = (Dir % 2 == 0) ? new Vector2(1, 0) : new Vector2(0, 1);
        Vectorspeed *= speed * (Dir <= 2 ? -1 : 1);
        _bala.GetComponent<Rigidbody2D>().velocity = Vectorspeed;
    }
  public  void ResetAnim()
    {
        currentWait = WaitTime;

        anim.SetBool("Shooting", true);

    }
}
