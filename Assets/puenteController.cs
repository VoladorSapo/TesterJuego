using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class puenteController : MonoBehaviour
{
 [SerializeField]   Tilemap tiles;
    [SerializeField] float waitseconds;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            tiles.animationFrameRate = 1;
            StartCoroutine(wait());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2*waitseconds/3);
        StartCoroutine(DebugLogController.Instance.AddLogs(DebugLogController.Instance.GetLogs("dondecaemos")));
        tiles.gameObject.GetComponent<TilemapRenderer>().enabled = false;

        yield return new WaitForSeconds(waitseconds / 3);
        tiles.gameObject.GetComponent<TilemapCollider2D>().enabled = false;


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
