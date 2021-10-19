using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock_spawn : MonoBehaviour
{   
    public GameObject rock;
    public int offset = 10;
    public float time = 5;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("createRock", offset, time);
    }

    void createRock(){
        Instantiate(rock, this.transform);
    }
}
