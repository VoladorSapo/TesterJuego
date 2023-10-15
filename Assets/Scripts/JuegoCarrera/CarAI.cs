using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    //[SerializeField] private float _distanceToSlowForWaypoint;
    //[SerializeField] private float _speedReductionOnWaypointAproach;
    [Header("Path")]
    public Waypoint CurrentWaypoint;
    public Waypoint PreviousWaypoint;
    [SerializeField] private float _divideSegments;

    [Header("Detect Cars")]
    public LayerMask carMask;
    public Transform HeadTransform;
    [SerializeField] private float _detectRadius;
    [SerializeField] private float _detectDistance;
    [SerializeField] private float _maxAvoidInfluence;
    [SerializeField] bool isAvoidingCars=true;
    [SerializeField] private float updateAvoidAngleTime;

    CarAIController _carController;
    public bool CanMove;


    Transform _targetToGo = null;
    Vector3 _targetPos;
    

    private void Awake()
    {
        _carController = GetComponent<CarAIController>();
        
        if(GameObject.Find("Waypoint0")!=null){
            CurrentWaypoint=GameObject.Find("Waypoint0").GetComponent<Waypoint>();
            PreviousWaypoint=CurrentWaypoint.PreviousWaypoints[0];
        }
    }

    private void Start()
    {
        PreviousWaypoint=CurrentWaypoint;
        _targetToGo = CurrentWaypoint.transform;
        CalculateDistanceToNextWaypoint();
    }

    void Update(){
        CalculateDistanceToNextWaypoint();
    }

    private void FixedUpdate()
    {
        
        FollowWaypoint();

        Vector2 inputVector = Vector2.zero;
            inputVector.y = ApplyThrottleOrBreak(TurnTowardTargetClamped());
            inputVector.x = TurnTowardTargetClamped();

        _carController.SetInputVector(inputVector);

    }

    void CalculateDistanceToNextWaypoint(){
        float dist=Vector3.Distance(new Vector3(transform.position.x,transform.position.y,0),new Vector3(CurrentWaypoint.transform.position.x,CurrentWaypoint.transform.position.y,0));
        this.gameObject.GetComponent<PositionRace>().DistanceToReachWaypoint=dist;
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
        //_carController.MaxSpeed = _initialMaxSpeed / _speedReductionOnWaypointAproach;
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
        vectorToTarget.Normalize();

        Vector2 vectorAvoidance=Vector2.zero;
        //Debug.LogWarning(vectorToTarget);
        

        if(isAvoidingCars){
            AvoidCars(vectorToTarget, out vectorAvoidance);
            if(vectorAvoidance.magnitude>(_targetPos - transform.position).magnitude){vectorToTarget=vectorAvoidance;}
        }
        
        

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
            distance/=_divideSegments;           
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

    bool IsCarInFrontOfAICar(out Vector3 position, out Vector3 otherCarRightVector){
       
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(HeadTransform.position+transform.up*0.5f,_detectRadius, transform.up, _detectDistance, carMask);
        if(raycastHit2D.collider!=null){
            Debug.DrawRay(transform.position, transform.up*_detectDistance, Color.red);

            position=raycastHit2D.collider.transform.position;

            otherCarRightVector=raycastHit2D.collider.transform.right;
            return true;
        }
        else{
            Debug.DrawRay(transform.position, transform.up*_detectDistance, Color.black);
        }
        position=Vector3.zero;
        otherCarRightVector=Vector3.zero;
        return false;
    }
    void AvoidCars(Vector2 vectorToTarget, out Vector2 newVectorToTarget){
        if(IsCarInFrontOfAICar(out Vector3 otherCarPosition, out Vector3 otherCarRightVector)){
            Vector2 avoidanceVector=Vector2.zero;

            avoidanceVector=Vector2.Reflect((otherCarPosition-transform.position).normalized,otherCarRightVector)
            *-Mathf.Sign( Vector3.Dot((otherCarPosition-transform.position).normalized,otherCarRightVector));

            float distanceToOtherCar=1/(otherCarPosition-transform.position).magnitude;

            Vector2 newVectorToTargetUnlerp=avoidanceVector;
            newVectorToTargetUnlerp.Normalize();

            float distanceToTarget=(_targetPos-transform.position).magnitude;
            float targetInfluence=Mathf.Clamp(_maxAvoidInfluence/distanceToTarget,0.5f,_maxAvoidInfluence);
            float avoidInfluence=_maxAvoidInfluence-targetInfluence;
            avoidInfluence/=distanceToOtherCar;

            newVectorToTarget=Quaternion.AngleAxis(_carController._turnAngles*distanceToOtherCar,Vector3.forward)*newVectorToTargetUnlerp;
            
            newVectorToTarget=Vector2.Lerp(newVectorToTarget,newVectorToTargetUnlerp,Time.fixedDeltaTime*updateAvoidAngleTime);
            newVectorToTarget.Normalize();
            newVectorToTarget*=distanceToOtherCar*20f;
            //Debug.DrawRay(transform.position,newVectorToTarget*10, Color.yellow);
            Debug.DrawRay(transform.position,newVectorToTarget*10,Color.green);
            
            return;
        }
        newVectorToTarget=vectorToTarget;
    }
}
