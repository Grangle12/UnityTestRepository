using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    EventManager gameOverEvent;


    [SerializeField] public GameObject mainMenuCanvasGO;
    [SerializeField] public GameObject gameOverMenuCanvasGO;
    [SerializeField] public GameObject highScoreCanvasGO;
    [SerializeField] public GameObject saveGGO;
    [SerializeField] public GameObject loadGGO;
    
    
    
    
    [SerializeField] public GameObject sGSlotGO;
    [SerializeField] public GameObject sGSlotParentGO;
    
    [SerializeField] public GameObject loadGSlotParentGO;


    [SerializeField] public GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;

    List<GameObject> sGSlotGOList = new List<GameObject>();
    List<GameObject> lGSlotGOList = new List<GameObject>();
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
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
        highScoreCanvasGO.SetActive(false);
        saveGGO.SetActive(false);
        loadGGO.SetActive(false);
        Debug.Log("OPENING MENU");
    }

    public void OpenSaveMenu()
    {
        saveGGO.SetActive(true);
        loadGGO.SetActive(false);
        mainMenuCanvasGO.SetActive(false);
        highScoreCanvasGO.SetActive(false);
        
        PopulateSaveMenu();
    }
    public void OpenLoadMenu()
    {
        loadGGO.SetActive(true);
        saveGGO.SetActive(false);
        mainMenuCanvasGO.SetActive(false);
        highScoreCanvasGO.SetActive(false);
        
        PopulateLoadMenu();
    }

    public void PopulateSaveMenu()
    {
        string[] nameList = SaveSystem.ListOfSaveFiles();

        sGSlotParentGO.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 30 * nameList.Length);

        bool inList = false;

        for (int i =0; i < nameList.Length; i++)
        {
            foreach(var GO in sGSlotGOList)
            {
                Debug.Log("we got: " + GO.GetComponentInChildren<TMP_Text>().text);
                if(GO.GetComponentInChildren<TMP_Text>().text == nameList[i])
                {
                    inList = true;
                }
            }
            if (!inList)
            {
                GameObject newGO = Instantiate(sGSlotGO, sGSlotParentGO.transform);
                newGO.GetComponentInChildren<TMP_Text>().text = nameList[i];
                sGSlotGOList.Add(newGO);
                newGO.GetComponent<Button>().onClick.AddListener(delegate () { SaveManager.instance.SaveGame(false); });
            }
            inList = false;
        }

    }
    public void PopulateLoadMenu()
    {
        //sGSlotGOList.Clear();

        string[] nameList = SaveSystem.ListOfSaveFiles();

        loadGSlotParentGO.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 30 * nameList.Length);

        bool inList = false;

        for (int i = 0; i < nameList.Length; i++)
        {
            foreach (var GO in lGSlotGOList)
            {
                Debug.Log("we got: " + GO.GetComponentInChildren<TMP_Text>().text);
                if (GO.GetComponentInChildren<TMP_Text>().text == nameList[i])
                {
                    inList = true;
                }
            }
            if (!inList)
            {
                GameObject newGO = Instantiate(sGSlotGO, loadGSlotParentGO.transform);
                newGO.GetComponentInChildren<TMP_Text>().text = nameList[i];
                lGSlotGOList.Add(newGO);
                newGO.GetComponent<Button>().onClick.AddListener(delegate () { SaveManager.instance.LoadGame(false); });
            }
            inList = false;
        }

    }


    public void CloseAllMenus()
    {
        saveGGO.SetActive(false);
        loadGGO.SetActive(false);
        mainMenuCanvasGO.SetActive(false);
        highScoreCanvasGO.SetActive(false);
        Debug.Log("CLOSING MENU");
        PauseManager.instance.UnpauseGame();
    }


    private void GameOverEvent_GameOver(object sender, EventManager.OnGameOverEventArges e)
    {
            Debug.Log("Game over event triggered");
            gameOverMenuCanvasGO.SetActive(true);

    }
}
