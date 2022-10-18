using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject pauseUI;
    public GameObject pauseSlideTop;
    public GameObject pauseSlideBottom;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();

            }

        }



    }

    public void Resume()
    {
        GamePaused = false;

        //pauseSlideTop.GetComponent<Animation>().play("MenuSlideTopReverse");
        //THIS IS SUCH AN EASY THING TO DO WHY CAN'T I FIGURE THIS OUT


        pauseUI.SetActive(false);
        pauseSlideTop.SetActive(false);
        pauseSlideBottom.SetActive(false);
        
        Time.timeScale = 1f;
        

       

    }

    void Pause()
    {
        GamePaused = true;
        pauseUI.SetActive(true);
        pauseSlideTop.SetActive(true);
        pauseSlideBottom.SetActive(true);


        Time.timeScale = 0f;
        

    }

}
