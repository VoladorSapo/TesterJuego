using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatormerPlayerController : MonoBehaviour
{

    [SerializeField] PlatformerRaycast raycasts;
    public float moveAxisX; //El input de izquierda y derecha
    public float moveAxisY; //El input de arriba y abajo
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] float speed;
    [SerializeField] float slowing;
    [SerializeField] float slowingair;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    [SerializeField] float turn;
    [SerializeField] float speedpower;
    [SerializeField] float jumpForce;
    [SerializeField] float touchgroundmax;
    [SerializeField] float touchground;
    [SerializeField] float pressjumpmax;
    [SerializeField] float pressjump;
    [SerializeField] float fallforce; //Pa que baje mas rapido
    [SerializeField] float gravityforce;
    [SerializeField] bool jumping;
    [SerializeField] Vector3 spawnPoint;
   [SerializeField] LayerMask damage;
    [SerializeField] LayerMask win;

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.layer);
        if( (damage.value & 1 << collision.gameObject.layer) > 0)
        {
            Die();
        }
        if (collision.gameObject.layer == win)
        {
            Die();
        }
    }
    public void Die()
    {
        transform.position = spawnPoint;
        rb2d.velocity = Vector2.zero;
    }
    public void Win()
    {

    }
    void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        raycasts = GetComponent<PlatformerRaycast>();
        rb2d = GetComponent<Rigidbody2D>();
        spawnPoint = GameObject.Find("SpawnPoint").transform.position;
        transform.position = spawnPoint;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressjump = pressjumpmax;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (rb2d.velocity.y > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.3f);
            }
        }
        pressjump -= Time.deltaTime;
        touchground -= Time.deltaTime;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        moveAxisX = Input.GetAxisRaw("Horizontal");
        if (moveAxisX > 0)
        {
            _sprite.flipX = true;
        }
        else if (moveAxisX < 0)
        {
            _sprite.flipX = false;
        }
        if (pressjump > 0 && touchground > 0)
        {
            jumping = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);

            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            touchground = 0;
            pressjump = 0;
        }
        if (raycasts.onSlope)
        {
            rb2d.gravityScale = 0;
            if (moveAxisX != 0)
            {
                rb2d.AddForce(gravityforce * -raycasts.slopeperpendicular * 0);
            }
            Debug.DrawRay(transform.position - new Vector3(0,0.5f), gravityforce * -raycasts.slopeperpendicular, Color.cyan);
        }
        else if (rb2d.velocity.y < 0)
        {
            rb2d.gravityScale = gravityforce * fallforce;
            Debug.DrawRay(transform.position - new Vector3(0, 0.5f), Vector3.down * rb2d.gravityScale * fallforce,Color.cyan);
        }
        else
        {
            Debug.DrawRay(transform.position - new Vector3(0, 0.5f), Vector3.down * rb2d.gravityScale,Color.cyan);

            rb2d.gravityScale = gravityforce;
        }
        if (moveAxisX > 0 && raycasts.rightwall || moveAxisX < 0 && raycasts.leftwall)
        {
        }
        else {
            Vector2 Velocity = rb2d.velocity;
            //if (!raycasts.onSlope)
            //{
            //    Velocity = rb2d.velocity.x;
            //}
            //else
            //{
            //    Velocity = rb2d
            //}
            if (raycasts.onSlope)
            {
               // print("cha");

                Velocity += raycasts.slopeperpendicular * -moveAxisX * -Mathf.Sign(raycasts.slopeperpendicular.x);//* Mathf.Cos(raycasts.groundangle * Mathf.Deg2Rad);
               // print(Velocity);
            }
            else
            {
                Velocity += new Vector2(moveAxisX,0);
               // print(Velocity);

            }
            if (Mathf.Abs(moveAxisX) < 0.01)
            {
                //print("decceleration");
                Velocity *= Mathf.Pow(1 - deceleration, speedpower) * slowing;
            }
           else if(Mathf.Sign(moveAxisX) != Mathf.Sign(Velocity.x))
            {
                //print("turn");
                Velocity *= Mathf.Pow(1 - turn, speedpower) * slowing;

            }
            else
            {
               // print("acceleration");
                Velocity *= Mathf.Pow(1 - acceleration, speedpower) * slowing;

            }
          //  print(Velocity);

            //// float rate = (Mathf.Abs(difftomax) <= Mathf.Abs(speed * moveAxisX)) ? acceleration : deceleration;
            //float rate =Mathf.Abs(moveAxisX) > 0 ? acceleration : deceleration;
            //print(rate);
            //float move = Mathf.Pow(Mathf.Abs(difftomax) * rate, speedpower) * Mathf.Sign(difftomax);
            //rb2d.AddForce(new Vector2(move * slowing, 0));
            if (raycasts.onSlope && moveAxisX == 0 && !jumping)

            {
                rb2d.velocity = Vector2.zero;

            }
            else if (!raycasts.ground || jumping)
            {

                rb2d.velocity = new Vector2(Velocity.x, rb2d.velocity.y);
            }
            else
            {
                print("helo");
                    rb2d.velocity = Velocity;
                //rb2d.velocity = new Vector2(0.9f * moveAxisX, rb2d.velocity.y);

            }
            Debug.DrawRay(transform.position, rb2d.velocity, Color.green);
        }
        if (raycasts.ground && rb2d.velocity.y <= 0.05f || (raycasts.onSlope && moveAxisX !=0))
        {
            print("tocando suelo");
            touchground = touchgroundmax;
            jumping = false;
            //rb2d.AddForce(new Vector2(Mathf.Sin( raycasts.groundangle * Mathf.Deg2Rad), Mathf.Cos(raycasts.groundangle * Mathf.Deg2Rad))*2f, ForceMode2D.Impulse);

        }

        

    }
}
