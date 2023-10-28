using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour {
    
    private float length, startposx, startposy;
    public GameObject cam;
    public float parallaxEffect;
    private float run = 0;

    void Start()
    {
        startposx = transform.position.x;
        startposy = transform.position.y;
        length = GetComponent<Renderer>().bounds.size.x;
    }


    void FixedUpdate()
    {
        
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distx = (cam.transform.position.x * parallaxEffect);
        float disty = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startposx + distx,transform.position.y, transform.position.z);
        
       
        
        if (temp > startposx + length) startposx += length;
        else if (temp < startposx - length) startposx -= length;
 
    }
}
