using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvasGO;
    [SerializeField] private GameObject gameOverMenuCanvasGO;


    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;


    // Start is called before the first frame update
    void Start()
    {
        mainMenuCanvasGO.SetActive(false);
        gameOverMenuCanvasGO.SetActive(false);
        //settingsMenuCanvasGO.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.MenuOpenInput)
        {
            if (!PauseManager.instance.isPaused)
            {
                Pause();
            }
        }
        else if (InputManager.instance.MenuCloseInput)
        {
            if (PauseManager.instance.isPaused)
            {
                Unpause();
            }
        
        }
        if (GameManager.instance.gameOver)
        {
            OpenGameOverMenu();
        }
    }


    //#region Pause/Unpause Functions

    public void Pause()
    {
        PauseManager.instance.PauseGame();
        OpenMainMenu();
    }

    public void Unpause()
    {

        CloseAllMenus();
        PauseManager.instance.UnpauseGame();
    }


    //#region Menu Stuff

    public void OpenMainMenu()
    {
        mainMenuCanvasGO.SetActive(true);
        Debug.Log("OPENING MENU");
    }
    private void CloseAllMenus()
    {
        mainMenuCanvasGO.SetActive(false);
        Debug.Log("CLOSING MENU");
    }

    private void OpenGameOverMenu()
    {
        gameOverMenuCanvasGO.SetActive(true);
    }
}
