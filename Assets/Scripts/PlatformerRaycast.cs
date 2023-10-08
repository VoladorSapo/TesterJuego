using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerRaycast : MonoBehaviour
{
    public PlatormerPlayerController player;
    public RaycastHit2D raycast_downmid;
    public RaycastHit2D raycast_downright;
    public RaycastHit2D raycast_downleft;
    public RaycastHit2D raycast_left_mid;
    public RaycastHit2D raycast_left_up;
    public RaycastHit2D raycast_left_down;
    public RaycastHit2D raycast_right_mid;
    public RaycastHit2D raycast_right_up;
    public RaycastHit2D raycast_right_down;
    public GameObject downcastpivot;
    public GameObject upcastpivot;
    [SerializeField] LayerMask layers;
    public bool ground;
    public bool leftwall;
    public bool rightwall;
    public float groundangle;
    public float groundangleold;
    public bool onSlope;
    public Vector2 slopeperpendicular;
    public float raycastdistside;
    public float raycastdistdown;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlatormerPlayerController>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(downcastpivot.transform.position, 0.1f);
        Gizmos.DrawSphere(upcastpivot.transform.position, 0.1f);
        Debug.DrawLine(transform.position, transform.position + raycastdistside * Vector3.left, Color.red);

    }
    // Update is called once per frame
    void Update()
    {
        raycast_left_mid = Physics2D.Raycast(player.transform.position, Vector2.left, raycastdistside, layers);
        Debug.DrawLine(transform.position, transform.position + raycastdistside * Vector3.left, Color.red);
        raycast_left_up = Physics2D.Raycast(upcastpivot.transform.position, Vector2.left, raycastdistside, layers);
        Debug.DrawLine(upcastpivot.transform.position, upcastpivot.transform.position + raycastdistside * Vector3.left, Color.red);
        raycast_left_down = Physics2D.Raycast(downcastpivot.transform.position, Vector2.left, raycastdistside, layers);
        Debug.DrawLine(downcastpivot.transform.position, downcastpivot.transform.position + raycastdistside * Vector3.left, Color.red);
        raycast_right_mid = Physics2D.Raycast(player.transform.position, Vector2.right, raycastdistside, layers);
        Debug.DrawLine(transform.position, transform.position + raycastdistside * Vector3.right, Color.red);
        raycast_right_up = Physics2D.Raycast(upcastpivot.transform.position, Vector2.right, raycastdistside, layers);
        Debug.DrawLine(upcastpivot.transform.position, upcastpivot.transform.position + raycastdistside * Vector3.right, Color.red);
        raycast_right_down = Physics2D.Raycast(downcastpivot.transform.position, Vector2.right, raycastdistside, layers);
        Debug.DrawLine(downcastpivot.transform.position, downcastpivot.transform.position + raycastdistside * Vector3.right, Color.red);
        raycast_downmid = Physics2D.Raycast(downcastpivot.transform.position, Vector2.down, raycastdistdown, layers);
        Debug.DrawLine(downcastpivot.transform.position, downcastpivot.transform.position + raycastdistdown * Vector3.down, Color.red);

        raycast_downleft = Physics2D.Raycast(downcastpivot.transform.position - new Vector3(0.5f, 0, 0), Vector2.down, raycastdistdown, layers);
        Debug.DrawLine(downcastpivot.transform.position - new Vector3(0.5f, 0, 0), downcastpivot.transform.position - new Vector3(0.5f, 0, 0) + raycastdistdown * Vector3.down, Color.red);

        raycast_downright = Physics2D.Raycast(downcastpivot.transform.position + new Vector3(0.5f, 0, 0), Vector2.down, raycastdistdown, layers);
        Debug.DrawLine(downcastpivot.transform.position + new Vector3(0.5f, 0, 0), downcastpivot.transform.position + new Vector3(0.5f, 0, 0) + raycastdistdown * Vector3.down, Color.red);

        ground = raycast_downmid || raycast_downleft || raycast_downright;
        leftwall = (raycast_left_down && raycast_downleft.normal != raycast_left_down.normal || raycast_left_mid && raycast_downleft.normal != raycast_left_mid.normal || raycast_left_up && raycast_downleft.normal != raycast_left_up.normal);

        rightwall = (raycast_right_mid && raycast_downright.normal != raycast_right_mid.normal || raycast_right_up && raycast_downright.normal != raycast_right_up.normal || raycast_right_down && raycast_downright.normal != raycast_right_down.normal);

        float angleleft = Vector2.Angle(raycast_downleft.normal, Vector2.up);
        float angleright = Vector2.Angle(raycast_downright.normal, Vector2.up);
        if(angleleft != 0)
        {
            groundangle = angleleft;
            slopeperpendicular = Vector2.Perpendicular(raycast_downleft.normal).normalized;
            groundangle *= Mathf.Sign(slopeperpendicular.y);
            slopeperpendicular *= Mathf.Sign(slopeperpendicular.y);
            Debug.DrawRay(raycast_downleft.point, slopeperpendicular, Color.blue);

        }
        else if(angleright != 0)
        {
            groundangle = angleright;
            slopeperpendicular = Vector2.Perpendicular(raycast_downright.normal).normalized;
            groundangle *= Mathf.Sign(slopeperpendicular.y);
            slopeperpendicular *= Mathf.Sign(slopeperpendicular.y);
            Debug.DrawRay(raycast_downright.point, slopeperpendicular, Color.blue);
        }
        else
        {
            groundangle = Vector2.Angle(raycast_downmid.normal, Vector2.up);
            slopeperpendicular = Vector2.Perpendicular(raycast_downmid.normal).normalized;
            groundangle *= Mathf.Sign(slopeperpendicular.y);
            slopeperpendicular *= Mathf.Sign(slopeperpendicular.y);
            Debug.DrawRay(raycast_downmid.point, slopeperpendicular, Color.blue);
        }
        if (groundangle != 0)
        {
            onSlope = true;
        }
        else
        {
            onSlope = false;
        }
     
        // (Vector2.Angle(Vector2.down, raycast_downmid.normal) - 180) * Mathf.Sign(raycast_downmid.normal.x);
    }
}
