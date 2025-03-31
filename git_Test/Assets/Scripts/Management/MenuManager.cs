using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    EventManager gameOverEvent;


    [SerializeField] public GameObject mainMenuCanvasGO;
    [SerializeField] public GameObject gameOverMenuCanvasGO;
    [SerializeField] public GameObject highScoreCanvasGO;


    [SerializeField] public GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;


    // Start is called before the first frame update
    void Start()
    {
        gameOverEvent = FindObjectOfType<EventManager>().GetComponent<EventManager>();
        gameOverEvent.OnGameOver += GameOverEvent_GameOver;
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


    private void GameOverEvent_GameOver(object sender, EventManager.OnGameOverEventArges e)
    {
            Debug.Log("Game over event triggered");
            gameOverMenuCanvasGO.SetActive(true);

    }
}
