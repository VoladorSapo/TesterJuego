using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMoveScript : MonoBehaviour, IPauseSystem
{
    
    //Tilemap
    public static PlayerMoveScript Instance;
    private Tilemap groundTileMap;
    
    [Header("Movimiento")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float moveSpeed;
    public bool Interacting=false;
    Vector2 moveDirection;
    Vector2 lastMoveDirection;

    [Header("Interacciones")]
    [SerializeField] Transform _interactionPos;
    [SerializeField] LayerMask _interactableMask;
    [SerializeField] SpriteRenderer _actionSprite;

    //Extras de movimiento
    [HideInInspector] public Vector2 forceToApply=Vector2.zero;
    private float forceDamping=1.2f;

    //Animator
    private Animator animator;

    void Awake(){
        if(Instance==null){
            Instance=this;
        }else{
            Destroy(this.gameObject);
        }
        _rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(this);

    }
    void Start(){
        if(GameObject.Find("GroundTilemap")!=null)
        groundTileMap=GameObject.Find("GroundTilemap").GetComponent<Tilemap>();
        
        SetPauseEvents();
        SetConversationEvents();
    }
    void Update(){
        HandleMovement();

        zeldaNPCBase npc;
        if(IsNPCNear(out npc) && !Interacting){
            
            _actionSprite.enabled=true;
            //En verdad tendría que haber un out int que sirva para elegir el sprite pero de momento se queda asi
            if(Input.GetKeyDown(KeyCode.E)){
                Interacting=true;
                Debug.Log("Ey buenas a todos, guapisimos, aqui vegetta777");
                npc.Say();
            }
        }else{
            _actionSprite.enabled=false;
        }
    }

    private bool IsNPCNear(out zeldaNPCBase npc)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_interactionPos.position, 0.8f, _interactableMask);

        Vector3 dir1=new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0); dir1.Normalize();
        Debug.DrawRay(_interactionPos.position,dir1,Color.black);

        Vector2 dir=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")); dir.Normalize();
        RaycastHit2D[] hits = Physics2D.RaycastAll(_interactionPos.position,dir,1f,_interactableMask);

            /*foreach (Collider2D collider in hitColliders)
            {
                if (collider.GetComponent<zeldaNPCBase>()!=null)
                {
                    npc=collider.GetComponent<zeldaNPCBase>();
                    return true;
                }
            }*/
            foreach(RaycastHit2D hit in hits){
                if(hit.transform.GetComponent<zeldaNPCBase>()!=null){
                    npc=hit.transform.GetComponent<zeldaNPCBase>();
                    return true;
                }
            }
            npc=null;
            return false;
    }

    void HandleMovement(){

        float H=Input.GetAxisRaw("Horizontal");
        float V=Input.GetAxisRaw("Vertical");

        if((H == 0 && V == 0) && moveDirection.x != 0 || moveDirection.y != 0)
        {
            lastMoveDirection = moveDirection;
        }
        if(Interacting){H=lastMoveDirection.x; V=lastMoveDirection.y;}
        
        moveDirection = new Vector2(H,V).normalized;

        animator.SetFloat("LastMoveH", lastMoveDirection.x);
        animator.SetFloat("LastMoveY", lastMoveDirection.y);
        animator.SetFloat("Horizontal", H);
        animator.SetFloat("Vertical", V);
        
        CheckTileProperty(H,V,out H, out V);

        
        Vector2 PlayerInput= new Vector2(H,V).normalized;
        Vector2 moveForce=PlayerInput*moveSpeed;
        if(Interacting){moveForce=Vector2.zero;}
        
        animator.SetFloat("Movement", moveForce.magnitude);
        moveForce+=forceToApply;
        moveForce/=forceDamping;

        if(Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <=0.01f){
            forceToApply=Vector2.zero;
        }
        _rb.velocity=moveForce;
    }

    void CheckTileProperty(float H, float V, out float H1, out float V1){
        if(groundTileMap==null){H1=H; V1=V; return;}
        Vector3 pos=transform.position;
        Vector3Int mapPos=new Vector3Int(Mathf.FloorToInt(pos.x),Mathf.FloorToInt(pos.y),0);
        Tile NormalTile=(Tile) CarreraManager.Instance.NormalTilemap.GetTile(CarreraManager.Instance.NormalTilemap.WorldToCell(mapPos));

        if(NormalTile!=null && NormalTile is StairTile){
            Vector2 newInputMovement=(NormalTile as StairTile).ModifyWalkingSpeed(H,V);
            H1=newInputMovement.x;
            V1=newInputMovement.y;
            return;
        }
        H1=H; V1=V; return;

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

    public void StartConversation(){
        Debug.LogWarning("INICIO");
        Interacting=true;
    }

    public void EndConversation(){
        Debug.LogWarning("FIN");
        Interacting=false;
    }
    
    public void SetConversationEvents(){
        DialogueController.Instance?.setEventConversations(StartConversation,EndConversation);
    }
}
