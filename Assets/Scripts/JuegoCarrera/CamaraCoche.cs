using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamaraCoche : MonoBehaviour
{ 
    public Transform _player;
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if(_player!=null){
            transform.position=new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
        }
        //No Rotation
        if(transform.parent!=null)
        transform.localRotation=Quaternion.identity;
    }
}
