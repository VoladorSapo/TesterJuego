using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIController : MonoBehaviour
{

    [Header("Settings")]
    public float MaxSpeed;
    [SerializeField] private float _accelerationFactor;
    [SerializeField] private float _driftFactor;
    [SerializeField] private float _turnFactor;
    [SerializeField] private float _turnLimitFactor;
    [SerializeField] private float _maintainTurnTime;
    private float _maintainTurnTimer;
    private float prevAngle=0;
    [SerializeField] private float _turnAngles;
    [SerializeField] private float _dragFactor;
    [SerializeField] private float _dragSpeed;

    float _accelerationInput;
    float _turnInput;

    float _rotationAngle=0;

    float _velocityVsUp;

    Rigidbody2D _rb;

    Vector2 _forceVector;
    Vector2 _forwardVelocity;
    Vector2 _rightVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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

        _forceVector = transform.up * _accelerationInput * _accelerationFactor;

        _rb.AddForce(_forceVector, ForceMode2D.Force);
        
    }

    void ApplyTurn()
    {
<<<<<<< Updated upstream
        if(_maintainTurnTimer>0){return;}

        _rotationAngle += -_turnInput * _turnFactor;
        _rb.MoveRotation(FixedAngle(_rotationAngle));
        if(prevAngle!=_rb.rotation){
            prevAngle=_rb.rotation;
            _maintainTurnTimer=_maintainTurnTime;
        }
=======
        _rotationAngle+=_turnInput*_turnFactor;
        float finalRotation=FixedAngle(_rotationAngle);
        _rb.rotation=finalRotation;
        Debug.Log(_rb.rotation);
>>>>>>> Stashed changes
    }
    float FixedAngle(float rotationAngle){
        if(rotationAngle<0){rotationAngle+=360f;}
        rotationAngle=rotationAngle%360f;

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
        _turnInput = inputVector.x;
        _accelerationInput = inputVector.y;
    }

}
