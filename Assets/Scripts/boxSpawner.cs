using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxSpawner : MonoBehaviour
{
  [SerializeField]  GameObject BoxPrefab;
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
        Box = Instantiate(BoxPrefab, transform.GetChild(0).position, Quaternion.identity);
        Box.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -fallspeed);
    }
}
