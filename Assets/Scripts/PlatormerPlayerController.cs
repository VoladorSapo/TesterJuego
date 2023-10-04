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
    [SerializeField] float speedpower;
    [SerializeField] float jumpForce;
    [SerializeField] float touchgroundmax;
    [SerializeField] float touchground;
    [SerializeField] float pressjumpmax;
    [SerializeField] float pressjump;
    [SerializeField] float fallforce; //Pa que baje mas rapido
    [SerializeField] float gravityforce;
    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        raycasts = GetComponent<PlatformerRaycast>();
        rb2d = GetComponent<Rigidbody2D>();
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
        if (moveAxisX > 0 && raycasts.rightwall || moveAxisX < 0 && raycasts.leftwall)
        {
        }
        else {
            print("ole beti");
            float difftomax = (speed * moveAxisX) - rb2d.velocity.x;
            float rate = (Mathf.Abs(difftomax) > 0.001) ? acceleration : deceleration;

            float move = Mathf.Pow(Mathf.Abs(difftomax) * rate, speedpower) * Mathf.Sign(difftomax);
            rb2d.AddForce(new Vector2(move * slowing, 0));
        }
        if (raycasts.ground && rb2d.velocity.y <= 0.5)
        {
            touchground = touchgroundmax;

        }

        if (pressjump > 0 && touchground > 0)
        {

            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);

            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            touchground = 0;
            pressjump = 0;
        }

        if (rb2d.velocity.y < 0)
        {
            rb2d.gravityScale = gravityforce * fallforce;
        }
        else
        {
            rb2d.gravityScale = gravityforce;
        }
    }
}
