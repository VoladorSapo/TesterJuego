using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportUp : MonoBehaviour
{
    float pos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlatormerPlayerController player = collision.GetComponent<PlatormerPlayerController>();
        if (player != null)
        {
            collision.transform.position = transform.GetChild(0).transform.position;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.GetChild(0).transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
