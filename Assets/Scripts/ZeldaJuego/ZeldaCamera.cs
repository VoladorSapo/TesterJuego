using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZeldaCamera : MonoBehaviour
{
    [SerializeField] Transform _player;
    void Start(){
        if(GameObject.FindGameObjectWithTag("Player")!=null)
        _player=GameObject.FindGameObjectWithTag("Player").transform;
    }
    void LateUpdate()
    {
        if(_player!=null){
            transform.position=new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
        }else if(GameObject.FindGameObjectWithTag("Player")!=null){
            _player=GameObject.FindGameObjectWithTag("Player").transform;
        }
        //No Rotation
        if(transform.parent!=null)
        transform.localRotation=Quaternion.identity;
    }
}
