using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Borja:

    [Header("Settings")]
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _accelerationFactor;
    [SerializeField] private float _driftFactor;
    [SerializeField] private float _turnFactor;
    [SerializeField] private float _turnAngles;
    [SerializeField] private float _turnLimitFactor;
    [SerializeField] private float _stopVelocityTreshhold;
    [SerializeField] private float _defaultDrag;
    [SerializeField] private float _dragFactor;
    [SerializeField] private float _dragSpeed;
    [SerializeField] private float _minSpeedToSkid;
    [SerializeField] private LayerMask _obstacleLayer;
    public float _accelerationInput;
    public float _turnInput;


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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _rb.drag = _defaultDrag;
    }

    private void FixedUpdate()
    {       
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

    void ApplyForce()
    {
        //if(derrailing){return;}
        _velocityVsUp = Vector2.Dot(transform.up, _rb.velocity);

        if (_velocityVsUp > _maxSpeed)
        {
            _rb.drag = Mathf.Lerp(_rb.drag, _dragFactor, Time.fixedDeltaTime * _dragSpeed);
        }
        else _rb.drag = _defaultDrag;

        if (_velocityVsUp > _maxSpeed && _accelerationInput > 0) return;
        if (_velocityVsUp < -_maxSpeed * 0.5 && _accelerationInput < 0) return;
        if (_rb.velocity.sqrMagnitude > _maxSpeed * _maxSpeed && _accelerationInput > 0) return;

        if (_accelerationInput == 0)
        {
            _rb.drag = Mathf.Lerp(_rb.drag, _dragFactor, Time.fixedDeltaTime * _dragSpeed);
            if (Mathf.Abs(_velocityVsUp) < _stopVelocityTreshhold) _rb.velocity = Vector2.zero;
        }
        else _rb.drag = _defaultDrag;

        _forceVector = transform.up * _accelerationInput * _accelerationFactor;

        _rb.AddForce(_forceVector, ForceMode2D.Force);
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
    }


    /*private void OnCollisionStay2D(Collision2D collision){
        if ((_obstacleLayer.value & (1 << collision.gameObject.layer)) != 0)
        {

        }
    }*/
}