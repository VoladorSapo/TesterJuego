using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CarAIController : MonoBehaviour, IPauseSystem
{

    [Header("Settings")]
    public float MaxSpeed;
    [SerializeField] private float _accelerationFactor;
    [SerializeField] private float _driftFactor;
    [SerializeField] private float _turnFactor;
    [SerializeField] private float _turnLimitFactor;
    [SerializeField] private float _turnReactionTime;
    [SerializeField] private float _maintainTurnTime;
    private float _maintainTurnTimer;
    public float _turnAngles;
    [SerializeField] private float _dragFactor;
    [SerializeField] private float _dragSpeed;

    [SerializeField] float _accelerationInput;
    [SerializeField] float _turnInput;
    float _currentTurnInput;

    [SerializeField] float _rotationAngle=0;

    float _velocityVsUp;

    Rigidbody2D _rb;

    Vector2 _forceVector;
    Vector2 _forwardVelocity;
    Vector2 _rightVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    void Start(){
        SetEvents();
    }
    private void FixedUpdate()
    {
        if(_maintainTurnTimer>0){_maintainTurnTimer-=Time.deltaTime;}
        ApplyForce();
        KillSideVelocity();
        ApplyTurn();
    }

    void ApplyForce()
    {

        DragControl();

        _forceVector = transform.up * _accelerationInput * _accelerationFactor;

        _rb.AddForce(_forceVector, ForceMode2D.Force);
        
    }

    void DragControl(){
        _velocityVsUp = Vector2.Dot(transform.up, _rb.velocity);

        if (_velocityVsUp > MaxSpeed)
        {         
            _rb.drag = Mathf.Lerp(_rb.drag, _dragFactor, Time.fixedDeltaTime * _dragSpeed);
        }
        else _rb.drag = 0;

        if (_velocityVsUp > MaxSpeed && _accelerationInput > 0) return;
        if (_velocityVsUp < -MaxSpeed * 0.5 && _accelerationInput < 0) return;
        if (_rb.velocity.sqrMagnitude > MaxSpeed * MaxSpeed && _accelerationInput > 0) return;

        if (_accelerationInput == 0)
        {
            _rb.drag = Mathf.Lerp(_rb.drag, _dragFactor, Time.fixedDeltaTime * _dragSpeed);
        }
        else _rb.drag = 0;

        if(HasTileDrag(transform.position)){
            _rb.drag = _dragFactor;
        }else{
            _rb.drag=0.2f;
        }
    }
    void ApplyTurn()
    {
        _currentTurnInput=Mathf.Lerp(_currentTurnInput,_turnInput,Time.deltaTime*_turnReactionTime);

        _rotationAngle+=_currentTurnInput*_turnFactor;
        float finalRotation=FixedAngle(_rotationAngle);
        _rb.SetRotation(finalRotation);
        

    }
    float FixedAngle(float rotationAngle){
        //rotationAngle=_rb.rotation+rotationAngle;
        rotationAngle=rotationAngle%360f;
        if(rotationAngle<0){rotationAngle+=360f;}
        


        int dividedAngle=(int)((rotationAngle+_turnAngles/2)/_turnAngles);
        float returnAngle=dividedAngle*_turnAngles;
        
        return returnAngle;

    }

    void KillSideVelocity()
    {
        _forwardVelocity = transform.up * Vector2.Dot(_rb.velocity, transform.up);
        _rightVelocity = transform.right * Vector2.Dot(_rb.velocity, transform.right);
        _rb.velocity = _forwardVelocity + _rightVelocity * _driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        //Debug.LogWarning(inputVector.x==0);
        _turnInput = -inputVector.x;
        _accelerationInput = inputVector.y;
    }
    public Vector2 GetVelocity(){
        return _rb.velocity;
    }


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

    bool HasTileDrag(Vector3 CarPos){
        if(CarreraManager.Instance.NormalTilemap==null){return false;}
        Vector3Int mapPos=new Vector3Int(Mathf.FloorToInt(CarPos.x),Mathf.FloorToInt(CarPos.y),0);
        Tile NormalTile=(Tile) CarreraManager.Instance.NormalTilemap.GetTile(CarreraManager.Instance.NormalTilemap.WorldToCell(mapPos));
        Tile GlicthedTile=(Tile) CarreraManager.Instance.GlitchedTilemap.GetTile(CarreraManager.Instance.GlitchedTilemap.WorldToCell(mapPos));

        
        if((NormalTile!=null && !CarreraManager.Instance.NoDragTiles.Contains(NormalTile)) || (GlicthedTile!=null && !CarreraManager.Instance.NoDragTiles.Contains(GlicthedTile))){
            //Debug.LogWarning(CarPos+", "+_NormalTilemap.WorldToCell(mapPos));
            return true;
        }else{
            
            return false;
        }

    }

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause,Unpause);
    }
}
