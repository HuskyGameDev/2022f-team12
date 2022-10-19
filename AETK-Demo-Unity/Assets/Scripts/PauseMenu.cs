using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject pauseUI;
    public GameObject pauseSlideTop;
    public GameObject pauseSlideBottom;

    public Animator topAnim;
    public Animator bottomAnim;


    void Start()
    {

    }




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


        topAnim.SetBool("Transition", false);
        bottomAnim.SetBool("Transition", false);

        Time.timeScale = 1f;
        

       

    }

    void Pause()
    {
        GamePaused = true;
        

        topAnim.SetBool("Transition", true);
        bottomAnim.SetBool("Transition", true);


        Time.timeScale = 0f;
        

    }

}
