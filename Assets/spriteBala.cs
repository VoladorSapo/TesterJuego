using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteBala : MonoBehaviour
{
    public void Boom()
    {
        GetComponentInParent<bulletController>().DestroySelf();
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
