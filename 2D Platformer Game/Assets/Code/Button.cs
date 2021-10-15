using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    bool pushed =false;
    public GameObject push;
    public GameObject laser;
    private void Update() {
        if(pushed){
            gameObject.SetActive(false);
            laser.SetActive(false);
            push.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="player"){
            pushed=true;
        }
    }

}
