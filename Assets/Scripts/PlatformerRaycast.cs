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
        Debug.DrawLine(transform.position, transform.position + raycastdistside * Vector3.left,Color.red);
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

        raycast_downleft = Physics2D.Raycast(downcastpivot.transform.position, Vector2.down, raycastdistdown, layers);
       // Debug.DrawRay(downcastpivot.transform.position, Vector2.down);

        raycast_downright = Physics2D.Raycast(downcastpivot.transform.position, Vector2.down, raycastdistdown, layers);
      //  Debug.DrawRay(downcastpivot.transform.position, Vector2.down);

        ground = raycast_downmid;
        leftwall = raycast_left_down || raycast_left_mid || raycast_left_up;
        rightwall = raycast_right_mid || raycast_right_up || raycast_right_down;

    }
}
