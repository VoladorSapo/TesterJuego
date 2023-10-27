using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReducirGravedad : MonoBehaviour
{

    [SerializeField] float velocidad;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlatormerPlayerController player = collision.GetComponent<PlatormerPlayerController>();
        if(player != null)
        {
            player.gravityforce = 0;
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocidad);
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
