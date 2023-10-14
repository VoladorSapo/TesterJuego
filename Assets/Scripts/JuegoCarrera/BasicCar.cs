using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCar : MonoBehaviour
{
    public bool canMove=false;
    public virtual void ActivateMovement(bool act){
        canMove=act;
    }
    
    protected virtual void Awake(){

    }
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate(){

    }
}
