using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MonoBehaviour
{
    [SerializeField] Vector3[] points;
    [SerializeField] int objective;
    GameObject movingObject;
    [SerializeField] float speed;
    [SerializeField] float spinspeed;
    [SerializeField] bool waitPlayer;
    bool startMoving;
    [SerializeField] bool canPause;
     bool paused;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 1; i < transform.childCount; i++)
        {
            Gizmos.DrawSphere(transform.GetChild(i).transform.position, 0.2f);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        startMoving = waitPlayer ? false : true;    
        SetEvents();
        paused = false;
        movingObject = transform.GetChild(0).gameObject;
        points = new Vector3[transform.childCount - 1];
        for (int i = 1; i <= points.Length; i++)
        {
            points[i - 1] = transform.GetChild(i).transform.position;
        }
        objective = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused && (!waitPlayer||startMoving))
        {
            if (points.Length > 0)
            {
                movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, points[objective], speed * Time.deltaTime);
                if (Vector2.Distance(movingObject.transform.position, points[objective]) < 0.01f)
                {
                    objective = (objective + 1) % points.Length;
                }
            }
            float angle = (movingObject.transform.eulerAngles.z + spinspeed * Time.deltaTime) % 360;
            movingObject.transform.eulerAngles = new Vector3(movingObject.transform.eulerAngles.x, movingObject.transform.eulerAngles.y, angle);
        }
    }

    public void StartMoving()
    {
        startMoving = true;
    }
    public void Pause()
    {
        if(canPause)
        paused = true;
    }

    public void Unpause()
    {
        if(canPause)
        paused = false;
    }

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }
}
