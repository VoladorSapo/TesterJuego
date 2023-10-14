using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxSpawner : MonoBehaviour
{
  [SerializeField]  GameObject BoxPrefab;
    [SerializeField] public GameObject fallPos;
    GameObject Box;
  [SerializeField]  float fallspeed;
    [SerializeField] LayerMask layers;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((layers.value & 1 << collision.gameObject.layer) > 0)
        {
            SpawnBox();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(fallPos.transform.position, 0.3f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnBox()
    {
        if(Box != null) { Destroy(Box.gameObject); }
        Box = Instantiate(BoxPrefab, transform.GetChild(0).position, Quaternion.identity,transform);
        Box.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -fallspeed);
    }
}
