using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    bool pushed =true;
    public GameObject Button;
    public GameObject laser;
    private void Update() {
        if(!pushed){
            gameObject.SetActive(false);
            laser.SetActive(true);
            Button.SetActive(true);
        }

    
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="player"){pushed=false;}
    }
    
}
