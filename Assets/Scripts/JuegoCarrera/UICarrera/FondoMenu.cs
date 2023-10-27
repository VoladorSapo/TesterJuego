using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class FondoMenu : MonoBehaviour
{
    [SerializeField] float moveX, moveY;
    [SerializeField] Material matCopy;
    private float x, y;
    
    Material mat;
    void Awake()
    {
       mat =Instantiate<Material>(matCopy);
       GetComponent<Image>().material=mat;
    }

    // Update is called once per frame
    void Update()
    {
        if(x+moveX*Time.deltaTime>1f){x=0f;}else{x+=moveX*Time.deltaTime;}
        if(y+moveY*Time.deltaTime>1f){y=0f;}else{y+=moveY*Time.deltaTime;}

        mat.SetFloat("_moveX",x);
        mat.SetFloat("_moveY",y);
    }
}
