using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    [Header("Scene Change")]
    public string nextScene;

    [Header("Position Change")]
    public Vector3 nextPosition;

    [Header("Music Change")]
    public string currentMusic;
    public string followingMusic;
    public float fadeOutTime, fadeInTime;

    void OnTriggerEnter2D(Collider2D other){
        if(other.GetComponent<PlayerMoveScript>()!=null){
            
            RoomManager.Instance.ChangeMusicTo(currentMusic,followingMusic,fadeOutTime,fadeInTime);
            RoomManager.Instance.ChangeToRoomAt(nextScene,nextPosition);
        }
    }
}
