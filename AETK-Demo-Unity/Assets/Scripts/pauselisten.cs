using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauselisten : MonoBehaviour
{
    public Canvas c1;
    public Canvas c2;

    bool menuShowing = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuShowing)
            {
                menuShowing = true;
                c1.gameObject.SetActive(true);
                c2.gameObject.SetActive(true);
            }
            else
            {
                menuShowing = false; 
                c1.gameObject.SetActive(false);
                c2.gameObject.SetActive(false);
            }
        }
      
    }
}
