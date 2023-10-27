using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CambiarZoomCamera : MonoBehaviour
{
    CinemachineVirtualCamera camera;
[SerializeField]    float value;
  [SerializeField]  float speed;
    bool start;
    int dir;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            start = true;
            if(camera.m_Lens.OrthographicSize > value)
            {
                dir = -1;
            }
            else
            {
                dir = 1;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            start = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        dir = 1;
        camera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(start && camera.m_Lens.OrthographicSize < value)
        {
            camera.m_Lens.OrthographicSize += dir* speed;
        }
        else
        {
            start = false;
        }
    }
}
