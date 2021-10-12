using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class player_scene2 : MonoBehaviour
{
    public int speed=4;
    public int jumpForce=500;
    public LayerMask groundLayer;
    public Transform feetPosition;
    public bool grounded =false;
    //public bool zoomout=false;
    float xSpeed=0;
    public GameObject removable_dark;
    public GameObject removable_light;
    Rigidbody2D _rigidbody;
    public bool Light=false;
    public CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xSpeed = Input.GetAxis("Horizontal")*speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        if((xSpeed<0 && transform.localScale.x>0)||(xSpeed>0 && transform.localScale.x<0)){
            transform.localScale*=new Vector2(-1,1);
        }
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
        grounded = Physics2D.OverlapCircle(feetPosition.position,.3f,groundLayer);
        if(Input.GetButtonDown("Jump") && grounded){
             _rigidbody.velocity = new Vector2(_rigidbody.velocity.x,0);
            _rigidbody.AddForce(new Vector2(0,jumpForce));
        }
        if(Input.GetKeyDown("1")){
            Light=!Light;
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("rock")){
            SceneManager.LoadScene("Scene2(Allen)");
        }
        if(other.gameObject.CompareTag("main")){
            vcam.m_Lens.OrthographicSize =7;
        }
    }
    //make a ground layer and feet for player
    //Buttonup getbutton buttondown
}
