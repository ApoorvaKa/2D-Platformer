using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button1;
    public GameObject button2;
    // Update is called once per frame

    AudioSource audioSource;
    void FixedUpdate()
    {
        if(!audioSource.isPlaying) audioSource.Play();
        else audioSource.Stop();
        
        if(!(button1.activeInHierarchy&&button2.activeInHierarchy)){
            gameObject.SetActive(false);
        }
        else{
            gameObject.SetActive(true);
        }
    }

}
