using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Canvas upgMenu;
    public Canvas pauseMenu;
    public Canvas logMenu;
    private bool upgMenuOpen = false;
    private bool pauseMenuOpen = false;
    private bool logMenuOpen = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if(!upgMenuOpen)
            {
                upgMenu.gameObject.SetActive(true);
                upgMenuOpen = true;
            }
            else
            {
                upgMenu.gameObject.SetActive(false);
                upgMenuOpen = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenuOpen)
            {
                pauseMenu.gameObject.SetActive(true);
                pauseMenuOpen = true;
            }
            else
            {
                pauseMenu.gameObject.SetActive(false);
                pauseMenuOpen = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!logMenuOpen)
            {
                logMenu.gameObject.SetActive(true);
                logMenuOpen = true;
            }
            else
            {
                logMenu.gameObject.SetActive(false);
                logMenuOpen = false;
            }
        }
    }
}
