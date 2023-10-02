using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    [SerializeField] private float _distanceToSlowForWaypoint;
    [SerializeField] private float _speedReductionOnWaypointAproach;

    public Waypoint CurrentWaypoint;
    public bool CarInProximity;
    public bool WaitingForGreenLight;

    CarAIController _carController;
      
    float _initialMaxSpeed;

    public bool CanMove;

    int _numberOfWaypoints;

    Transform _targetWaypoint = null;


    private void Awake()
    {
        _carController = GetComponent<CarAIController>();
    }

    private void Start()
    {
        _targetWaypoint = CurrentWaypoint.transform;
        _initialMaxSpeed = _carController.MaxSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;
        if (CanMove)
        {
            inputVector.x = TurnTowardTarget();
            inputVector.y = ApplyThrottleOrBreak(inputVector.x);
        }
        else _carController.MaxSpeed = 0;

        _carController.SetInputVector(inputVector);
        OnWaypointAproached();
    }

    private void OnWaypointAproached()
    {
       
            float distance = Vector3.Distance(transform.position, CurrentWaypoint.transform.position);
            Debug.Log((distance < CurrentWaypoint._distanceToReachWaypoint)+", "+distance+", "+CurrentWaypoint._distanceToReachWaypoint);
            if (distance < CurrentWaypoint._distanceToReachWaypoint)
            {
                    NextWaypoint();
            }

            if (CarInProximity)
            {
                if (_carController.MaxSpeed == _initialMaxSpeed) ReduceSpeed();
            }
            else
            {
                if (_carController.MaxSpeed < _initialMaxSpeed / _speedReductionOnWaypointAproach)
                {
                    _carController.MaxSpeed = _initialMaxSpeed;
                }else if (distance > _distanceToSlowForWaypoint && _carController.MaxSpeed != _initialMaxSpeed)
                {
                    _carController.MaxSpeed = _initialMaxSpeed;
                }
            }

            if (!CurrentWaypoint._carSlowsDown) return;

            if (distance < _distanceToSlowForWaypoint && _carController.MaxSpeed == _initialMaxSpeed)
            {
                ReduceSpeed();
            }
              
    }

    public void ReduceSpeed()
    {
        _carController.MaxSpeed = _initialMaxSpeed / _speedReductionOnWaypointAproach;
    }

    private float TurnTowardTarget()
    {
        Vector2 vectorToTarget = CurrentWaypoint.transform.position - transform.position;
        //Debug.LogWarning(vectorToTarget);
        vectorToTarget.Normalize();

        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        //Debug.Log(angleToTarget);
        
        float steerAmount = Mathf.Clamp(-angleToTarget/90f, -1, 1);
        //Debug.LogWarning(steerAmount);
        return steerAmount;
    }

    private float ApplyThrottleOrBreak(float inputX)
    {
        return 1.05f - Mathf.Abs(inputX) / 1;
    }

    public void NextWaypoint()
    {
        Debug.LogWarning("CA;B");
        CurrentWaypoint=CurrentWaypoint.FollowingWaypoints[0];
        _targetWaypoint=CurrentWaypoint.transform;
    }

    private Waypoint PickRandom(List<Waypoint> followingWaypoints)
    {
        float rValue=UnityEngine.Random.Range(0f,1f);
        int index=-1;
        int i=0;
        do{
            rValue-=followingWaypoints[i].WPweight;
            index++;
            i++;
        }while(rValue>0);

        return followingWaypoints[index];
    }
}
