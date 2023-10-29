using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        this.GetComponent<Canvas>().renderMode=RenderMode.ScreenSpaceOverlay;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
