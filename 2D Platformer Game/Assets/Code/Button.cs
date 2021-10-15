using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject push;
    private void OnTriggerStay2D(Collider2D other) {
            gameObject.SetActive(false);

    }

}
