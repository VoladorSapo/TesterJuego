using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : IStateClass
{

    public override void OnEnterState(zeldaNPCBase npc)
    {

    }


    public override void OnExitState(zeldaNPCBase npc)
    {
        
    }

    public override void StateEffect(zeldaNPCBase npc)
    {
        if(npc.allWaypoints.Length>1){
                npc.transform.position=Vector2.MoveTowards(npc.transform.position,npc.allWaypoints[npc.currentWP].pos,npc.walkingSpeed*Time.deltaTime);
                //Cuando este el animator
                npc.SetDirection(npc.allWaypoints[npc.currentWP].pos);

                if(Vector2.Distance(npc.transform.position,npc.allWaypoints[npc.currentWP].pos)<=0.0001f){
                    npc.currentWP=npc.allWaypoints[npc.currentWP].PickRandom();
                }
        }
    }

    public override IStateClass TransitionTo(zeldaNPCBase npc, string param)
    {
        switch(param){
                case "Staring": OnExitState(npc); IStateClass newState0 = new StaringState(); newState0.OnEnterState(npc); return newState0;
                case "Frozen": OnExitState(npc); IStateClass newState1 = new FrozenState(); newState1.OnEnterState(npc); return newState1;
                default: return this;
        }
    }
}
