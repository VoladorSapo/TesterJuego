using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward,0.1f);
    }
}
