using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class player : MonoBehaviour
{
    int speed = 8;
    int jumpForce = 600;
    public int numJumps = 1;
    Rigidbody2D _rigidbody;
    public LayerMask groundLayer;
    public Transform feet;
    public bool onGround = false;
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
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);

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
            numJumps = 1;
        }

        if(Input.GetButtonDown("Jump") && (onGround || numJumps > 0)){
            // zeroing out the velocity makes sure that a double jump can't happen
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
            numJumps--;
        }

        // press "1" to switch from colors (currently only two)
        if(Input.GetButtonDown("Fire2")){
            Light=!Light;
        }

        animator.SetFloat("Speed",Mathf.Abs(xSpeed));
    }

    // detecting death to reload and change the view of the cinemachine
    // we can change the level player reloads to later.
    // Either first level or current level

    private void OnCollisionEnter2D(Collision2D other) {
      //if(other.gameObject.CompareTag("rock")){
      //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      //}
      if(other.gameObject.CompareTag("main")){
          vcam.m_Lens.OrthographicSize =7;
      }
        if (other.gameObject.CompareTag("enemy")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("enemy")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.CompareTag("proximity vanish")){
            Destroy(other.gameObject);
        }
    }
}
