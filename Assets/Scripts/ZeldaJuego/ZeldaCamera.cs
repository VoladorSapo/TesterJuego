using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZeldaCamera : MonoBehaviour
{
    [SerializeField] Transform _player;
    void Start(){
        _player=GameObject.FindGameObjectWithTag("ZeldaPlayer").transform;
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
