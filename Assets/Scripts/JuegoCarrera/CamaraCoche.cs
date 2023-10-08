using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamaraCoche : MonoBehaviour
{ 
    [SerializeField] Transform _player;
    void Awake(){
        _player=GameObject.Find("PlayerCar").transform;
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
