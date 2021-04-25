using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Canvas upgMenu;
    public Canvas pauseMenu;
    public Canvas pauseMenuOptions;
    public Canvas logMenu;
    public ScannerController scannerController;
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
            if(!upgMenuOpen && !pauseMenuOpen)
            {
                upgMenu.gameObject.SetActive(true);
                upgMenuOpen = true;
                logMenu.gameObject.SetActive(false);
                logMenuOpen = false;
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
                logMenu.gameObject.SetActive(false);
                logMenuOpen = false;
                upgMenu.gameObject.SetActive(false);
                upgMenuOpen = false;
            }
            else
            {
                pauseMenu.gameObject.SetActive(false);
                pauseMenuOptions.gameObject.SetActive(false);
                pauseMenuOpen = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!logMenuOpen && !pauseMenuOpen)
            {
                logMenu.gameObject.SetActive(true);
                logMenuOpen = true;
                logMenu.GetComponentInChildren<ScannerMenu>().OpenMenu(scannerController.getEntityDatabase());
                upgMenu.gameObject.SetActive(false);
                upgMenuOpen = false;
            }
            else
            {
                logMenu.gameObject.SetActive(false);
                logMenuOpen = false;
            }
        }
    }
}
