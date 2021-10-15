using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public GameObject Button;
    public GameObject laser;

    private void OnTriggerExit2D(Collider2D other) {
            Button.SetActive(true);
            laser.SetActive(true);
    }
    
}
