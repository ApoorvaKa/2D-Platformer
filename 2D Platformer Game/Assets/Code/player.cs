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
    AudioSource audioSource;
    //using for animator parameters
    public Animator animator;
    //boolean for switching color meachinsim
    public bool Light=false;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        audioSource=GetComponent<AudioSource>();
    }


    void FixedUpdate()
    {
        xSpeed = Input.GetAxis("Horizontal") * speed;
        // changing direction is made faster here on the ground
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        if((xSpeed<0 && transform.localScale.x>0)||(xSpeed>0 && transform.localScale.x<0)){
            transform.localScale*=new Vector2(-1,1);
        }

        //sound effect for walking
        if(xSpeed!=0){
            if(!audioSource.isPlaying)audioSource.Play();
        }
        else audioSource.Stop();

        //remove and reappear the object based on boolean "light"
        //remove its collider and make it semi-transparent
        if(Light){
            foreach(Transform child in removable_light.gameObject.transform){
                child.gameObject.GetComponent<BoxCollider2D>().isTrigger=true;
                //only affect the rigidbody for boxes
                if(child.tag=="box")Destroy(child.gameObject.GetComponent<Rigidbody2D>());
                else if(child.tag=="rock hole") child.transform.GetChild(0).gameObject.SetActive(false);
                Color current=child.gameObject.GetComponent<SpriteRenderer>().color;
                current.a=0.3f;
                child.gameObject.GetComponent<SpriteRenderer>().color=current;
            }
            foreach(Transform child in removable_dark.gameObject.transform){
                child.gameObject.GetComponent<BoxCollider2D>().isTrigger=false;
                //only affect the rigidbody for boxes (when they don't have rigidbody)
                if(child.tag=="box"){
                    if(child.gameObject.GetComponent<Rigidbody2D>()==null){
                        child.gameObject.AddComponent<Rigidbody2D>();
                        child.gameObject.GetComponent<Rigidbody2D>().constraints= RigidbodyConstraints2D.FreezeRotation;
                    }
                }
                else if(child.tag=="rock hole") child.transform.GetChild(0).gameObject.SetActive(true);
                Color current=child.gameObject.GetComponent<SpriteRenderer>().color;
                current.a=1f;
                child.gameObject.GetComponent<SpriteRenderer>().color=current;
            }

            //removable_dark.gameObject.SetActive(true);
            //removable_light.gameObject.SetActive(false);

        }
        else{
            foreach(Transform child in removable_dark.gameObject.transform){
                child.gameObject.GetComponent<BoxCollider2D>().isTrigger=true;
                //only affect the rigidbody for boxes
                if(child.tag=="box")Destroy(child.gameObject.GetComponent<Rigidbody2D>());
                else if(child.tag=="rock hole") child.transform.GetChild(0).gameObject.SetActive(false);
                Color current=child.gameObject.GetComponent<SpriteRenderer>().color;
                current.a=0.3f;
                child.gameObject.GetComponent<SpriteRenderer>().color=current;
            }
            foreach(Transform child in removable_light.gameObject.transform){
                child.gameObject.GetComponent<BoxCollider2D>().isTrigger=false;
                //only affect the rigidbody for boxes (when they don't have rigidbody)
                if(child.tag=="box"){
                    if(child.gameObject.GetComponent<Rigidbody2D>()==null){
                        child.gameObject.AddComponent<Rigidbody2D>();
                        child.gameObject.GetComponent<Rigidbody2D>().constraints= RigidbodyConstraints2D.FreezeRotation;
                    }
                }
                else if(child.tag=="rock hole") child.transform.GetChild(0).gameObject.SetActive(true);
                Color current=child.gameObject.GetComponent<SpriteRenderer>().color;
                current.a=1f;
                child.gameObject.GetComponent<SpriteRenderer>().color=current;
            }
            //removable_dark.gameObject.SetActive(false);
            //removable_light.gameObject.SetActive(true);
            
        }
    }

    void Update(){

        if(publicVars.paused) return;

        onGround = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);
        //worked for slope in lv3 and lv4
        if(onGround && _rigidbody.velocity.y>=0.15)animator.SetBool("Isjump",true);
        else animator.SetBool("Isjump",!onGround);
        //print(_rigidbody.velocity.y);
        //animator.SetBool("Isjump",!onGround);
        if(onGround){
            numJumps = 1;
            //animator.SetBool("Isjump",false);
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
        if(other.gameObject.CompareTag("rock")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.CompareTag("enemy")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.CompareTag("sloped")){
            _rigidbody.freezeRotation = false;
        }
        else{

        }
    }

    //for laser
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("enemy")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionExit2D(Collision2D other){
        if (other.gameObject.CompareTag("sloped")){
            _rigidbody.freezeRotation = true;
            _rigidbody.rotation = 0f;
        }
    }
}
