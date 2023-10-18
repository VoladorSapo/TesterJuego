using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenState : IStateClass
{
    public override void OnEnterState(zeldaNPCBase npc)
    {

    }


    public override void OnExitState(zeldaNPCBase npc)
    {
        
    }

    public override void StateEffect(zeldaNPCBase npc)
    {
        
    }

    public override IStateClass TransitionTo(zeldaNPCBase npc, string param)
    {
        switch(param){
                case "Moving": OnExitState(npc); IStateClass newState0 = new MovingState(); newState0.OnEnterState(npc); return newState0;
                case "Staring": OnExitState(npc); IStateClass newState1 = new StaringState(); newState1.OnEnterState(npc); return newState1;
                default: return this;
        }
    }
}
