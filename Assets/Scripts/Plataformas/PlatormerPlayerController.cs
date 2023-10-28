using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
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
    [SerializeField] float doublejumpForce;
    [SerializeField] float touchgroundmax;
    [SerializeField] float touchground;
    [SerializeField] float pressjumpmax;
    [SerializeField] float pressjump;
    [SerializeField] public float fallforce; //Pa que baje mas rapido
    [SerializeField] public float gravityforce;
    [SerializeField] bool jumping;
    [SerializeField] bool canDoubleJump;
    [SerializeField] bool fallingfrombox;
    [SerializeField] bool insideFloor;
    [SerializeField] bool dead;
    [SerializeField] int extraJumps;
    [SerializeField] int currentextraJumps;
    [SerializeField] Vector3 spawnPoint;
    [SerializeField] LayerMask damage;
    [SerializeField] LayerMask win;
    [SerializeField] Animator anim;
    [SerializeField] bool paused;
    [SerializeField] int Monedas;
  [SerializeField]  TMP_Text Contador;
    Vector2 saveVelocity;
    // Start is called before the first frame update


    private void Awake()
    {
        paused = false;
        anim = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!paused)
        {
            if ((damage.value & 1 << collision.gameObject.layer) > 0)
            {
                Die();
            }
        }
        if ((win.value & 1 << collision.gameObject.layer) > 0)
        {
            Win();
        }
        if(collision.gameObject.tag == "Moneda")
        {
            AudioManager.Instance.PlaySound("Coin", false, transform.position, false);

            Destroy(collision.gameObject);
            Monedas++;
            Contador.text = Monedas.ToString();

        }
        if(collision.gameObject.tag == "CheckPoint")
        {
            spawnPoint = collision.gameObject.transform.position;
        }
    }
    public void Die()
    {
        print("die");
        currentextraJumps = 0;
        dead = true;
        AudioManager.Instance.PlaySound("Death", false, transform.position, false);
        anim.SetBool("Die", true);
        if (CamaraGlobal.Instance._player == name && GameObject.FindGameObjectsWithTag("Player").Length == 1)
        {   
                FindObjectOfType<CinemachineBrain>().enabled = false;    
        }
        transform.parent = null;
        rb2d.velocity = Vector2.zero;
    }
    public void Respawn()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(gameObject);
        }
        insideFloor = false;
        transform.position = spawnPoint;
        dead = false;
        if (CamaraGlobal.Instance._player == name)
        {
            FindObjectOfType<CinemachineBrain>().enabled = true;
        }
    }
    public void Win()
    {
        print("ganasteLight");
    }
    void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint").transform.position;
        SetEvents();

        insideFloor = false;
        fallingfrombox = false;
        currentextraJumps = 0;
        _sprite = GetComponentInChildren<SpriteRenderer>();
        raycasts = GetComponent<PlatformerRaycast>();
        rb2d = GetComponent<Rigidbody2D>();
        transform.position = spawnPoint;
        print(spawnPoint);
        CamaraGlobal.Instance.attachedCanvas.platformUI.SetActive(true);
        Contador = GameObject.Find("ContadorMonedas").GetComponent<TMP_Text>();
        Monedas = 0;
        Contador.text = Monedas.ToString();


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !paused)
        {
            pressjump = pressjumpmax;
        }
        if (Input.GetKeyUp(KeyCode.Space) && !paused)
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
        if (!raycasts.ground)
        {
            anim.SetBool("onGround", false);

        }
        if (!fallingfrombox && !paused && !dead)
        {
            moveAxisX = Input.GetAxisRaw("Horizontal");
            if (moveAxisX > 0)
            {
                _sprite.flipX = false;
                anim.SetBool("walking", true);

            }
            else if (moveAxisX < 0)
            {
                _sprite.flipX = true;
                anim.SetBool("walking", true);

            }
            else
            {
                anim.SetBool("walking", false);

            }
            if (raycasts.ground && rb2d.velocity.y <= 0.05f || (raycasts.onSlope && moveAxisX != 0 && pressjump > 0))
            {
                anim.SetBool("onGround",true);
                anim.SetBool("doubleJump", false);
                anim.SetBool("jump", false);
                touchground = touchgroundmax;
                currentextraJumps = 0;
                jumping = false;
                //rb2d.AddForce(new Vector2(Mathf.Sin( raycasts.groundangle * Mathf.Deg2Rad), Mathf.Cos(raycasts.groundangle * Mathf.Deg2Rad))*2f, ForceMode2D.Impulse);

            }
            if ( pressjump > 0 && (touchground > 0 || (canDoubleJump && currentextraJumps < extraJumps)))
            {
                float force = jumpForce;
                if (jumping)
                {
                    force = doublejumpForce;
                    anim.SetBool("doubleJump", true);
                    currentextraJumps++;
                }
                else
                {
                    anim.SetBool("doubleJump", false);

                }
                if( touchground < 0)
                {
                    currentextraJumps++;
                }
                jumping = true;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
                touchground = 0;
                anim.SetBool("onGround", false);
                AudioManager.Instance.PlaySound("Jump", false, transform.position, false);
                anim.SetBool("jump", true);
                pressjump = 0;

            }
            if (raycasts.upbox && !insideFloor)
            {
                transform.position =new Vector3(transform.position.x, raycasts._boxSpawn.fallPos.transform.position.y,-1);
                rb2d.velocity = Vector2.zero;
                insideFloor = true;
            }
            if (raycasts.onSlope)
            {
                rb2d.gravityScale = 0;
                if (moveAxisX != 0)
                {
                    rb2d.AddForce(gravityforce * -raycasts.slopeperpendicular * 0);
                }
                Debug.DrawRay(transform.position - new Vector3(0, 0.5f), gravityforce * -raycasts.slopeperpendicular, Color.cyan);
            }
            else if (rb2d.velocity.y < 0)
            {
                rb2d.gravityScale = gravityforce * fallforce;
                Debug.DrawRay(transform.position - new Vector3(0, 0.5f), Vector3.down * rb2d.gravityScale * fallforce, Color.cyan);
            }
            else
            {
                Debug.DrawRay(transform.position - new Vector3(0, 0.5f), Vector3.down * rb2d.gravityScale, Color.cyan);

                rb2d.gravityScale = gravityforce;
            }
            if (raycasts.movingplatform)
            {
                if(!transform.parent != raycasts.movingplatform.transform){
                    raycasts.movingplatform.GetComponentInParent<MovingObjectController>().StartMoving();
                }
                transform.parent = raycasts.movingplatform.transform;
            }
            else
            {
                 transform.parent = null;
            }
            if (moveAxisX > 0 && raycasts.rightwall || moveAxisX < 0 && raycasts.leftwall)
            {
            }
            else
            {
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
                    Velocity += new Vector2(moveAxisX, 0);
                    // print(Velocity);

                }
                if (Mathf.Abs(moveAxisX) < 0.01)
                {
                    //print("decceleration");
                    Velocity *= Mathf.Pow(1 - deceleration, speedpower) * slowing;
                }
                else if (Mathf.Sign(moveAxisX) != Mathf.Sign(Velocity.x))
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
                    rb2d.velocity = Velocity;
                    //rb2d.velocity = new Vector2(0.9f * moveAxisX, rb2d.velocity.y);

                }
                Debug.DrawRay(transform.position, rb2d.velocity, Color.green);
            }
            anim.SetFloat("yVelocity", rb2d.velocity.y);
            if(rb2d.velocity.y < 0)
            {
                anim.SetBool("doubleJump", false);
                anim.SetBool("jump", false);
            }

        }

    }
    private void OnDestroy()
    {
        PauseController.Instance?.UnSetPausedEvents(Pause, Unpause);

    }
    public void Pause()
    {
        paused = true;
        anim.speed = 0;
        saveVelocity = rb2d.velocity;
        rb2d.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        rb2d.gravityScale = 0;
    }

    public void Unpause()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        paused = false;
        anim.speed = 1;
        rb2d.velocity = saveVelocity;
    }

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }
}
