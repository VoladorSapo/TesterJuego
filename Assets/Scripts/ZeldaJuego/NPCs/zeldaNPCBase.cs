using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zeldaNPCBase : MonoBehaviour
{
    [SerializeField] private string conversationKey;
    [SerializeField] bool canMove=false;

    [SerializeField] NPCWaypoint[] allWaypoints;
    [SerializeField] float walkingSpeed;
    int currentWP;

    void Update(){
        if(canMove){
            if(allWaypoints.Length>1){
                Vector2.MoveTowards(transform.position,allWaypoints[currentWP].pos,walkingSpeed*Time.deltaTime);
            }
        }
    }
    public void Say(){
        DialogueController.Instance.getConversation(conversationKey);
    }
}
