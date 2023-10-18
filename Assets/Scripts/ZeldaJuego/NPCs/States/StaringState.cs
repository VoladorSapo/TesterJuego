using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaringState : IStateClass
{
    public override void OnEnterState(zeldaNPCBase npc)
    {
        Debug.LogWarning("uhaudh");
    }


    public override void OnExitState(zeldaNPCBase npc)
    {
        
    }

    public override void StateEffect(zeldaNPCBase npc)
    {
        if(GameObject.Find("ZeldaPlayer")!=null && Vector2.Distance(npc.transform.position,GameObject.Find("ZeldaPlayer").transform.position)<=3f)
        npc.SetDirection(GameObject.Find("ZeldaPlayer").transform.position);
    }

    public override IStateClass TransitionTo(zeldaNPCBase npc, string param)
    {
        switch(param){
                case "Moving": OnExitState(npc); IStateClass newState0 = new MovingState(); newState0.OnEnterState(npc); return newState0;
                case "Frozen": OnExitState(npc); IStateClass newState1 = new FrozenState(); newState1.OnEnterState(npc); return newState1;
                default: return this;
        }
    }
}
