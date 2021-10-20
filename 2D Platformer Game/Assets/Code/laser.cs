using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button1;
    public GameObject button2=null;
    // Update is called once per frame

    AudioSource audioSource;

    private void Start() {
        audioSource=GetComponent<AudioSource>();
    }

    private void Update() {
        //print(audioSource.isPlaying);
        if(!audioSource.isPlaying) audioSource.Play();
    }
    void FixedUpdate()
    {
        
        if(!(button1.activeInHierarchy&&button2.activeInHierarchy)){
            gameObject.SetActive(false);
        }
        else{
            gameObject.SetActive(true);
        }
    }

}
