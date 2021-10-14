using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
   public int level;
    private void OnCollisionEnter2D(Collision2D other) {
        
        if (other.gameObject.CompareTag("player")){
            SceneManager.LoadScene("Level"+level);
        }
    }
}
