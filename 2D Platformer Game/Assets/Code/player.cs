using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

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

    //the cinemachine object
    public CinemachineVirtualCamera vcam;

    //gameobjects for color (currently only dark and light)
    public GameObject removable_dark;
    public GameObject removable_light;

    //using for animator parameters
    public Animator animator;
    //boolean for switching color meachinsim
    public bool Light=false;
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

        //turning the player direction
        if((xSpeed<0 && transform.localScale.x>0)||(xSpeed>0 && transform.localScale.x<0)){
            transform.localScale*=new Vector2(-1,1);
        }
    
        //remove and reappear the object based on boolean "light"
        if(Light){
            removable_dark.gameObject.SetActive(true);
            removable_light.gameObject.SetActive(false);
        }
        else{
            removable_dark.gameObject.SetActive(false);
            removable_light.gameObject.SetActive(true);
        }
        Vector3 euler = transform.eulerAngles;
        if (euler.z > 180) euler.z = euler.z - 360;
        euler.z = Mathf.Clamp(euler.z, -15, 15);
        transform.eulerAngles = euler;
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

        // press "1" to switch from colors (currently only two)
        if(Input.GetKeyDown("1")){
            Light=!Light;
        }
    
        animator.SetFloat("Speed",Mathf.Abs(xSpeed));
    }

    // detecting death to reload and change the view of the cinemachine
    // we can change the level player reloads to later. 
    // Either first level or current level

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("rock")){
            SceneManager.LoadScene("Scene2(Allen)");
        }
        if(other.gameObject.CompareTag("main")){
            vcam.m_Lens.OrthographicSize =7;
        }
        if (other.gameObject.CompareTag("enemy")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
