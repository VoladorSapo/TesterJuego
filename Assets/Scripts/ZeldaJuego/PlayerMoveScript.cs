using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMoveScript : MonoBehaviour, IPauseSystem
{
    
    //Tilemap
    private Tilemap groundTileMap;
    
    [Header("Movimiento")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float moveSpeed;
    public bool Interacting=false;

    [Header("Interacciones")]
    [SerializeField] Transform _interactionPos;
    [SerializeField] LayerMask _interactableMask;
    [SerializeField] SpriteRenderer _actionSprite;

    //Extras de movimiento
    [HideInInspector] public Vector2 forceToApply=Vector2.zero;
    private float forceDamping=1.2f;

    void Awake(){
        _rb=GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(this);
    }
    void Start(){
        if(GameObject.Find("GroundTilemap")!=null)
        groundTileMap=GameObject.Find("GroundTilemap").GetComponent<Tilemap>();
        
        SetEvents();
    }
    void Update(){
        HandleMovement();

        zeldaNPC npc;
        if(IsNPCNear(out npc) && !Interacting){
            
            _actionSprite.enabled=true;
            //En verdad tendría que haber un out int que sirva para elegir el sprite pero de momento se queda asi
            if(Input.GetKeyDown(KeyCode.E)){
                Debug.Log("Ey buenas a todos, guapisimos, aqui vegetta777");
                npc.Say();
            }
        }else{
            _actionSprite.enabled=false;
        }
    }

    private bool IsNPCNear(out zeldaNPC npc)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_interactionPos.position, 0.8f, _interactableMask);

            foreach (Collider2D collider in hitColliders)
            {
                if (collider.GetComponent<zeldaNPC>()!=null)
                {
                    npc=collider.GetComponent<zeldaNPC>();
                    return true;
                }
            }
            npc=null;
            return false;
    }

    void HandleMovement(){
        if(Interacting){return;}
        float H=Input.GetAxisRaw("Horizontal");
        float V=Input.GetAxisRaw("Vertical");
        
        CheckTileProperty(H,V,out H, out V);

        Vector2 PlayerInput= new Vector2(H,V).normalized;
        Vector2 moveForce=PlayerInput*moveSpeed;

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

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause,Unpause);
    }
}
