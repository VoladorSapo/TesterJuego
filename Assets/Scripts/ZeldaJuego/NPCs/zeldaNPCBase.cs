using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class zeldaNPCBase : MonoBehaviour, IPauseSystem 
{
    [SerializeField] private string conversationKey;

    [Header("Posicion Pies")]
    public Transform feetT;

    [Header("Estados NPC")]
    [SerializeField] public NPCWaypoint[] allWaypoints;
    [SerializeField] public float walkingSpeed;
    [HideInInspector] public int currentWP;
    public LayerMask playerLayer;

    [SerializeField] string startingState;
    private IStateClass currentState;
    public Animator anim;

  

    void Awake(){
        switch(startingState){
            case "Moving": currentState=new MovingState(); break;
            case "Staring": currentState=new StaringState(); break;
            case "Frozen": currentState=new FrozenState(); break;
        }
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }

    }
    void Update(){
        
        if(currentState!=null)
            currentState.StateEffect(this);

    }

    public void SetDirection(Vector2 target)
    {
        Vector2 dir=target-new Vector2(transform.position.x,transform.position.y);
        if (GetComponent<Animator>() != null)
        {
            //dir.Normalize();
            anim.SetFloat("h", dir.x);
            anim.SetFloat("v", dir.y);
        }
        /*if(Mathf.Abs(dir.x)>Mathf.Abs(dir.y)){
            //Derecha o Izquierda
        }else if(Mathf.Abs(dir.y)>Mathf.Abs(dir.x)){
            //Arriba o Abajo
        }*/
    }

    public void Say(){
        DialogueController.Instance.setEventConversations(StartConversation,EndConversation);
        
        DialogueController.Instance.getConversation(conversationKey);
    }

    public void StartConversation(){
        currentState=currentState.TransitionTo(this,"Staring");
    }

    public void EndConversation(){
        currentState=currentState.TransitionTo(this,startingState);
        DialogueController.Instance.unsetEventConverstaions(StartConversation,EndConversation);
    }

    public void Pause()
    {
        this.enabled=false;
    }

    public void Unpause()
    {
        this.enabled=true;
    }

    public void SetPauseEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause,Unpause);
    }

}
