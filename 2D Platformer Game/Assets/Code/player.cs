using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
public int speed;
    public int jumpForce = 500;
    public int numJumps = 0;
    Rigidbody2D _rigidbody;
    public LayerMask groundLayer;
    public Transform feet;
    public bool onGround = false;
    float xSpeed = 0;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        if((xSpeed<0 && transform.localScale.x>0) || (xSpeed>0 && transform.localScale.x<0))
        {
            transform.localScale *= new Vector2(-1,1);
        }
    }

    void Update(){
        onGround = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);
        if(onGround){
            numJumps = 1;
        }
    
        if(Input.GetButtonDown("Jump") && (numJumps > 0 || onGround)){
            // zeroing out the velocity makes sure that a double jump can't happen
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
            numJumps--;
        }
    }
}
