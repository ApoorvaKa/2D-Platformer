using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int speed = 10;
    public int maxSpeed = 50;
    public int jumpForce = 200;
    public int dashForce = 250;
    public int numJumps = 0;
    Rigidbody2D _rigidbody;
    public LayerMask groundLayer;
    public Transform feet;
    public bool onGround = false;
    public bool hasDash = false;
    float xSpeed = 0;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        xSpeed = Input.GetAxis("Horizontal") * speed;
        // changing direction is made faster here on the ground
        //if(_rigidbody.velocity.x/xSpeed < 0 && onGround){
        //    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x/1.1f, _rigidbody.velocity.y);
        //}

        // Movement slightly acceleration based now, mainly to avoid horizonal velocity resetting
        if(Mathf.Abs(_rigidbody.velocity.x) < maxSpeed){
            _rigidbody.AddForce(new Vector2(xSpeed,0));
        }

    }

    void Update(){
        onGround = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);
        if(onGround){
            numJumps = 0;
            hasDash = true; // You get only can dash once before touching the ground
        }
    
        if(Input.GetButtonDown("Jump") && (onGround || numJumps > 0)){
            // zeroing out the velocity makes sure that a double jump can't happen
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
            numJumps--;
        }

        // Dash is set to Fire1 for now
        // Dash applies a force in the direction the player is moving
        if(Input.GetButtonDown("Fire1") && hasDash){
            Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            // zeroing out the velocity to make the dash more noticible
            _rigidbody.velocity = new Vector2(0,1);

            // Setting dash magnitude
            direction.Normalize();
            direction *= dashForce;
            _rigidbody.AddForce(direction);
            hasDash = false;
        }
    }
}
