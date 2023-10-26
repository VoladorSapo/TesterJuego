using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class CarController : BasicCar, IPauseSystem 
{

    [Header("Settings")]
    public bool isGlitchedCar=false;
    [SerializeField] private Material glitchedMat;

    [Header("Factors")]
    [SerializeField] private float _defaultMaxSpeed;
    private float _maxSpeed;
    [SerializeField] private float _accelerationFactor;
    [SerializeField] private float _driftFactor;
    [SerializeField] private float _turnFactor;
    [SerializeField] private float _turnAngles;
    [SerializeField] private float _turnLimitFactor;
    [SerializeField] private float _stopVelocityTreshhold;

    [Header("Drag")]
    [SerializeField] private float _defaultDrag;
    [SerializeField] private float _dragFactor;
    [SerializeField] private float _dragSpeed;
    [SerializeField] private float _minSpeedToSkid;

    
    [Header("Obstacles")]
    [SerializeField] private LayerMask _carLayer;
    [SerializeField] private LayerMask _obstacleLayer;



    [Header("Controls")]
    public float _accelerationInput;
    public float _turnInput;

    [Header("Waypoint")]
    [HideInInspector] public List<Waypoint> CurrentWaypoint;
    [HideInInspector] public float DistanceToReachWaypoint;


    [Header("\nPlayerInput")]
    [SerializeField] PlayerInput _plyInput;
    
    //Sonidos
    //bool isMotorSound;

    float _realRotationAngle=0;
    float _velocityVsUp;

    Rigidbody2D _rb;

    Vector2 _forceVector;
    Vector2 _forwardVelocity;
    Vector2 _rightVelocity;

    protected override void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        if(GameObject.Find("Waypoint0")!=null){
            CurrentWaypoint= new List<Waypoint>
            {
                GameObject.Find("Waypoint0").GetComponent<Waypoint>()
            };
            CalculateDistanceToNextWaypoint();
        }
    }
    protected override void Start()
    {
        _rb.drag = _defaultDrag;

        if(PauseController.Instance!=null)
        SetPauseEvents();
    }

    protected override void Update(){
        CalculateDistanceToNextWaypoint();
        
        if(_rb.velocity.magnitude>=5.5f && isGlitchedCar){
            Physics2D.IgnoreLayerCollision(11,14);
        }else{
            Physics2D.IgnoreLayerCollision(11,14,false);
        }
    }
    protected override void FixedUpdate()
    {     
        if(!canMove) return;
        
        
        ApplyForce();
        KillSideVelocity();
        ApplyTurn();

        /*if(!isMotorSound && _accelerationInput>0 && _rb.velocity.magnitude>0.25f){
            AudioManager.Instance.PlaySound("Motor",true,transform.position,true);
            isMotorSound=true;
        }else if(isMotorSound && _accelerationInput<=0 && _rb.velocity.magnitude<=0.25f){
          AudioManager.Instance.StopAllSoundsWithName("Motor");
            isMotorSound=false;
        }*/

        //if(Input.GetKeyDown(KeyCode.E)){AudioManager.Instance.PlaySound("Horn",false,transform.position,true);}
    }

    void CalculateDistanceToNextWaypoint(){
        if(CurrentWaypoint==null){return;}
        
        float minDist=9999f;
        foreach(Waypoint wp in CurrentWaypoint){
            if(wp==null){continue;}
            float wpDist=Vector3.Distance(new Vector3(transform.position.x,transform.position.y,0),new Vector3(wp.transform.position.x,wp.transform.position.y,0));
            if(wpDist<minDist){
                minDist=wpDist;
            }
        }
        this.gameObject.GetComponent<PositionRace>().DistanceToReachWaypoint=minDist;
    }
    void ApplyForce()
    {
        //if(derrailing){return;}
       

        DragControl();

        
        _forceVector = transform.up * _accelerationInput * _accelerationFactor;

        _rb.AddForce(_forceVector, ForceMode2D.Force);
    }

    void DragControl(){
         
        _velocityVsUp = Vector2.Dot(transform.up, _rb.velocity);

        if (_velocityVsUp > _maxSpeed)
        {
            _rb.drag = Mathf.Lerp(_rb.drag, _dragFactor, Time.fixedDeltaTime * _dragSpeed);
        }
        else _rb.drag = _defaultDrag;

        if (_velocityVsUp > _maxSpeed && _accelerationInput > 0) return;
        if ((_velocityVsUp < -_maxSpeed * 0.5 && _accelerationInput < 0) && !isGlitchedCar)return;
        if (_rb.velocity.sqrMagnitude > _maxSpeed * _maxSpeed && _accelerationInput > 0) return;

        if (_accelerationInput == 0)
        {
            _rb.drag = Mathf.Lerp(_rb.drag, _dragFactor, Time.fixedDeltaTime * _dragSpeed);
            if (Mathf.Abs(_velocityVsUp) < _stopVelocityTreshhold) _rb.velocity = Vector2.zero;
        }
        else _rb.drag = _defaultDrag;

        if(HasTileDrag(transform.position)){
            _rb.drag = _dragFactor;
        }else{
            _rb.drag=_defaultDrag;
        }
    }


    void ApplyTurn()
    {
        _realRotationAngle+=-_turnInput*_turnFactor;
        _rb.SetRotation(FixedAngle(_realRotationAngle));
    }

    float FixedAngle(float rotationAngle){
        if(rotationAngle<0){rotationAngle+=360f;}
        rotationAngle=rotationAngle%360f;

        int dividedAngle=(int)((rotationAngle+_turnAngles/2)/_turnAngles);
        float returnAngle=dividedAngle*_turnAngles;
        return returnAngle;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    
    public void DoCollide(float velocity, Vector2 contactPoint){
        //Vector2 contactPoint=collision.contacts[0].point;
        Vector2 pos=transform.position;
        
    }
    public void DoCollide(float velocity, Vector2 contactPoint, float minVelocity){
        //Vector2 contactPoint=collision.contacts[0].point;
        if(velocity<minVelocity){return;}
        Vector2 pos=transform.position;
    }
    public void Collide(bool isCar)
    {      
        if (isCar) Debug.Log("coche");
        else _plyInput.GoBack();
    }

    public bool AreTiresScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = Vector2.Dot(transform.right, _rb.velocity);
        isBraking = false;

        if (_accelerationInput < 0 && _velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        if (Mathf.Abs(lateralVelocity) > _minSpeedToSkid)
        {
            return true;
        }

        return false;
    }

    void KillSideVelocity()
    {
        _forwardVelocity = transform.up * Vector2.Dot(_rb.velocity, transform.up);
        _rightVelocity = transform.right * Vector2.Dot(_rb.velocity, transform.right);
        _rb.velocity = _forwardVelocity + _rightVelocity * _driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        _turnInput = inputVector.x;
        _accelerationInput = inputVector.y;
        if(_accelerationInput<0 && isGlitchedCar){
            _maxSpeed=10f;
        }else{
            _maxSpeed=_defaultMaxSpeed;
        }
    }



    //Pausa
    Vector2 storedSpeed;
    public void Pause(){
        storedSpeed=_rb.velocity;
        _rb.velocity=Vector2.zero;
        this.enabled=false;
    }

    public void Unpause(){
        this.enabled=true;
        _rb.velocity=storedSpeed;
    }

    public void SetPauseEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause,Unpause);
    }

    //Otros
    bool HasTileDrag(Vector3 CarPos){
        if(CarreraManager.Instance.NormalTilemap==null || CarreraManager.Instance.GlitchedTilemap==null || (_accelerationInput<0 && isGlitchedCar)){return false;}

        Vector3Int mapPos=new Vector3Int(Mathf.FloorToInt(CarPos.x),Mathf.FloorToInt(CarPos.y),0);
        TileBase NormalTile=CarreraManager.Instance.NormalTilemap.GetTile(CarreraManager.Instance.NormalTilemap.WorldToCell(mapPos));
        TileBase GlicthedTile=CarreraManager.Instance.GlitchedTilemap.GetTile(CarreraManager.Instance.GlitchedTilemap.WorldToCell(mapPos));

        
        
        if(((NormalTile!=null && !CarreraManager.Instance.NoDragTiles.Contains(NormalTile)) || (GlicthedTile!=null && !CarreraManager.Instance.NoDragTiles.Contains(GlicthedTile)))
        && !((GlicthedTile!=null && CarreraManager.Instance.NoDragTiles.Contains(GlicthedTile) && CarreraManager.Instance.GlitchedTilemap.GetComponent<TilemapRenderer>().enabled) && (NormalTile!=null && !CarreraManager.Instance.NoDragTiles.Contains(NormalTile)))
        ){
            
            return true;
        }else{
            
            return false;
        }

    }

    public void GlicthedCar(){
        Material mat=Instantiate<Material>(glitchedMat);
        
        mat.SetFloat("_moveX",0.35f);
        mat.SetFloat("_moveY",1f);
        mat.SetFloat("_ValueX",35f);
        mat.SetFloat("_ValueY",925f);
        GetComponent<SpriteRenderer>().material=mat;
        isGlitchedCar=true;
    }

    

    /*private void OnCollisionStay2D(Collision2D collision){
        if ((_obstacleLayer.value & (1 << collision.gameObject.layer)) != 0)
        {

        }
    }*/
}
