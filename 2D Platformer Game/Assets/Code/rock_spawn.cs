using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock_spawn : MonoBehaviour
{   
    public GameObject rock;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("createRock", 4, 2);
    }

    void createRock(){
        Instantiate(rock, this.transform);
    }
}
