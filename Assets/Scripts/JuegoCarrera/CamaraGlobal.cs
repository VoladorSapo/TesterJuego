using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraGlobal : MonoBehaviour
{
    public static CamaraGlobal Instance;
    public CameraEffects cameraFX;
    [SerializeField] private Transform _player;
    void Awake(){

        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }
    public void SetPlayer(Transform player){
        _player=player;
    }
    public void SetCameraPosition(Vector3 pos){
        this.transform.position=pos;
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
