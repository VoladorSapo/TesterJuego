using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IStateClass{
    public abstract void StateEffect(zeldaNPCBase npc);
    public abstract IStateClass TransitionTo(zeldaNPCBase npc, string param);
    public abstract void OnEnterState(zeldaNPCBase npc);
    public abstract void OnExitState(zeldaNPCBase npc);
}
