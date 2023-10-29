using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    [SerializeField] float spinspeed = 100;
    bool paused;
    // Start is called before the first frame update
    void Start()
    {
        SetEvents();
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            float angle = (transform.eulerAngles.z + spinspeed * Time.fixedDeltaTime) % 360;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        }
    }
    public void Pause()
    {
        paused = true;    }

    public void Unpause()
    {
        paused = false;
    }

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }
}
