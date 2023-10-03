using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatormerPlayerController : MonoBehaviour
{
    public float moveAxisX; //El input de izquierda y derecha
    public float moveAxisY; //El input de arriba y abajo
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] float speed;
    [SerializeField] float slowing;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    [SerializeField] float speedpower;
    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
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

        float difftomax = (speed * moveAxisX) - rb2d.velocity.x;
        print(difftomax);
        float rate = (Mathf.Abs(difftomax) > 0.001) ? acceleration : deceleration;

        float move = Mathf.Pow(Mathf.Abs(difftomax) * rate, speedpower) * Mathf.Sign(difftomax);
        rb2d.AddForce(new Vector2(move * slowing,0));
    }
}
