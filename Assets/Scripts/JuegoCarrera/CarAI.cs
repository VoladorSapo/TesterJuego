using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    [SerializeField] private float _distanceToSlowForWaypoint;
    [SerializeField] private float _speedReductionOnWaypointAproach;

    public Waypoint CurrentWaypoint;
    public Waypoint PreviousWaypoint;
    public bool CarInProximity;
    public bool WaitingForGreenLight;

    CarAIController _carController;
      
    float _initialMaxSpeed;

    public bool CanMove;

    int _numberOfWaypoints;

    Transform _targetToGo = null;
    Vector3 _targetPos;
    

    private void Awake()
    {
        _carController = GetComponent<CarAIController>();
    }

    private void Start()
    {
        
        _initialMaxSpeed = _carController.MaxSpeed;
        PreviousWaypoint=CurrentWaypoint;
        _targetToGo = CurrentWaypoint.transform;
    }

    private void FixedUpdate()
    {
        FollowWaypoint();

        Vector2 inputVector = Vector2.zero;
            inputVector.y = ApplyThrottleOrBreak(TurnTowardTargetClamped());
            inputVector.x = TurnTowardTargetClamped();

        _carController.SetInputVector(inputVector);

        OnWaypointAproached();
    }

    private void OnWaypointAproached()
    {
       
            float distance = Vector3.Distance(transform.position, CurrentWaypoint.transform.position);
            
            if (distance <= CurrentWaypoint._distanceToReachWaypoint)
            {
                    NextWaypoint();
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

        float angleToTarget = -Vector2.SignedAngle(transform.up, vectorToTarget);
        //Debug.Log(angleToTarget);
        
        //float steerAmount = Mathf.Clamp(angleToTarget/45f, -1, 1);
        //Debug.LogWarning(steerAmount);
        return angleToTarget/3.5f;
    }

    private float TurnTowardTargetClamped(){
        Vector2 vectorToTarget = _targetPos - transform.position;
        //Debug.LogWarning(vectorToTarget);
        vectorToTarget.Normalize();

        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        //Debug.Log(angleToTarget);
        
        float steerAmount = Mathf.Clamp(-angleToTarget/45f, -1, 1);
        //Debug.LogWarning(steerAmount);
        return steerAmount;
    }


    void FollowWaypoint(){
        if(CurrentWaypoint!=null){
            _targetToGo=CurrentWaypoint.transform;
            //_targetPos=CurrentWaypoint.transform.position;

            //float distanceToWaypoint=(_targetPos-transform.position).magnitude;
            Vector3 nearestPointOnLine=FindNearestPointOnLine(PreviousWaypoint.transform.position, CurrentWaypoint.transform.position, transform.position);
            Vector3 distance=CurrentWaypoint.transform.position-transform.position;
            distance/=10;
                float segments=(nearestPointOnLine-transform.position).magnitude/10f;            
                _targetPos=nearestPointOnLine+distance;
                     
            
            Debug.DrawLine(transform.position, _targetPos, Color.cyan);
        }
    }
    
    Vector3 FindNearestPointOnLine(Vector3 lineStart, Vector3 lineEnd, Vector3 pos){
        Vector3 lineHeadingEnd=(lineEnd-lineStart);
        float maxDist=lineHeadingEnd.magnitude;
        lineHeadingEnd.Normalize();

        Vector3 lineVectorStartToPos=pos-lineStart;
        float dotProduct=Vector2.Dot(lineVectorStartToPos,lineHeadingEnd);
        dotProduct=Mathf.Clamp(dotProduct,0f,maxDist);

        return lineStart + lineHeadingEnd*dotProduct;
    }

    private float ApplyThrottleOrBreak(float inputX)
    {

        return 5.05f - Mathf.Abs(inputX) / 1;
    }

    public void NextWaypoint()
    {
        PreviousWaypoint=CurrentWaypoint;
        CurrentWaypoint=CurrentWaypoint.FollowingWaypoints[0];
        _targetToGo=CurrentWaypoint.transform;
    }

    Vector2 FindNearestPointOnLine(Vector2 lineStartPos, Vector2 lineEndPos, Vector2 point){
        Vector2 lineToEnd=lineEndPos-lineStartPos;
        lineEndPos.Normalize();

        //Projection
        Vector2 lineVectorStartToPoint=point-lineStartPos;
        float dotProduct=Vector2.Dot(lineVectorStartToPoint,lineToEnd);
        dotProduct=Mathf.Clamp(dotProduct,0f,7f);

        return lineStartPos + lineToEnd*dotProduct;
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
