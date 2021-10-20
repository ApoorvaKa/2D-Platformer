using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rock : MonoBehaviour
{
    public Vector2 startForce;
    Rigidbody2D _rigidbody;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(startForce);
        audioSource = GetComponent<AudioSource>();
    }

    //Sound effect
    void Update(){
        if(_rigidbody.velocity.x!=0){
            if(!audioSource.isPlaying)audioSource.Play();
        }
        else audioSource.Stop();
        //print(_rigidbody.velocity.x);
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("player") && _rigidbody.velocity.magnitude > 1){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
