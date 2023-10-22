using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraGlobal : MonoBehaviour
{
    public static CamaraGlobal Instance;
    public CameraEffects cameraFX;
    public CanvasUI attachedCanvas;
    public string _player;
    void Awake(){

        if(CamaraGlobal.Instance==null){
            
            Instance=this;
            DontDestroyOnLoad(this.gameObject);

            attachedCanvas=transform.GetChild(0).gameObject.GetComponent<CanvasUI>();
            print("setactivent");
            attachedCanvas.platformUI.gameObject.SetActive(false);
            attachedCanvas.carUI.gameObject.SetActive(false);
            attachedCanvas.zeldaUI.gameObject.SetActive(false);
        }else{
            Destroy(this.gameObject);
        }
    }
    public void SetCameraPosition(Vector3 pos){
        this.transform.position=pos;
    }
    void Update()
    {
        if(GameObject.Find(_player)!=null){
            Transform player=GameObject.Find(_player).transform;
            if(player!=null){
                transform.position=new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            }
        }
        //No Rotation
        if(transform.parent!=null)
        transform.localRotation=Quaternion.identity;
    }
}
