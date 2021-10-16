using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject quitButton;
    void Start(){
        Resume();
        
        #if UNITY_WEBGL
        quitButton.SetActive(false);
        #endif
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (publicVars.paused){
                Resume();

            }
            else{
                pauseMenu.SetActive(true);
                publicVars.paused = true;
                Time.timeScale = 0;
            }
        }
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        publicVars.paused = false;
        Time.timeScale = 1;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit(){
        Application.Quit();
    }



}
