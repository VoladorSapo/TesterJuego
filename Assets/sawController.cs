using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawController : MonoBehaviour
{
   [SerializeField] Vector3[] points;
    [SerializeField]int objective;
    GameObject saw;
    [SerializeField] float speed;
    [SerializeField] float spinspeed;
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
        saw = transform.GetChild(0).gameObject;
        points = new Vector3[transform.childCount - 1];
        for (int i = 1; i <= points.Length; i++)
        {
            points[i-1] = transform.GetChild(i).transform.position;
        }
        objective = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Length > 0)
        {
            saw.transform.position = Vector3.MoveTowards(saw.transform.position, points[objective], speed * Time.deltaTime);
            if (Vector2.Distance(saw.transform.position, points[objective]) < 0.01f)
            {
                objective = (objective + 1) % points.Length;
            }
        }
        float angle = (saw.transform.eulerAngles.z + spinspeed * Time.deltaTime) % 360;
        saw.transform.eulerAngles = new Vector3(saw.transform.eulerAngles.x, saw.transform.eulerAngles.y, angle);
    }
}
