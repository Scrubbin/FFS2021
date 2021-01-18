using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private GameObject player;

    private GameObject UI;

    private GameObject menu;

    private bool buttonHeld;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        UI = GameObject.Find("UI");
        menu = GameObject.Find("MenuCanvas");
        menu.SetActive(false);
        buttonHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        var menuInput = Input.GetAxisRaw("Menu");
        if (menuInput == 0)
        {
            buttonHeld = false;
        }
        
        if (menuInput > 0 && buttonHeld == false)
        {
            buttonHeld = true;
            if (menu.activeSelf == false)
            {
                Time.timeScale = 0;
                menu.SetActive(true); 
            }
            else
            {
                Time.timeScale = 1;
                menu.SetActive(false);
            }
            
        } 
    }
}
