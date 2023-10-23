using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
   [SerializeField] LayerMask layersHit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((layersHit.value & 1 << collision.gameObject.layer) > 0) && collision.gameObject.transform != transform.parent && collision.gameObject.transform.parent != transform.parent)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
