using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCannons : MonoBehaviour
{
    [SerializeField] canonController[] canons;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            foreach (canonController canon in canons)
            {
                canon.ResetAnim();
                canon.currentWait = 0;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
