using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public GameObject Button;
    public GameObject laser;

    private void FixedUpdate() {
        
    }
    private void OnTriggerExit2D(Collider2D other) {
            Button.SetActive(true);
            laser.SetActive(true);
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag=="box"&&other.gameObject.GetComponent<BoxCollider2D>().isTrigger){
            //print(other.gameObject.GetComponent<BoxCollider2D>().isTrigger);
            Button.SetActive(true);
            laser.SetActive(true);
        }
    }
    
}
