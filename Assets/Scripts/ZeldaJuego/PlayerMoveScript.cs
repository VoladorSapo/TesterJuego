using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMoveScript : MonoBehaviour, IPauseSystem
{
    //Tilemap
    private Tilemap groundTileMap;
    //Movimiento base
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float moveSpeed;

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
