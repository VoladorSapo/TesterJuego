using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    public string nextScene;
    public Vector3 nextPosition;

    void OnTriggerEnter2D(Collider2D other){
        if(other.GetComponent<PlayerMoveScript>()!=null){
            RoomManager.Instance.ChangeToRoomAt(nextScene,nextPosition);
        }
    }
}
