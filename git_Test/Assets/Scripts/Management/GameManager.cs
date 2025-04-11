using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

   

    //SceneManager currentScene;

    [SerializeField] public GameObject carGameObject;

    public CarMovement playerUnit;
    public float currentTime;
   
    //Coins
    public SerializableDictionary<int, bool> coinDictionary = new SerializableDictionary<int, bool>();
    public List<CoinCollection> coins = new List<CoinCollection>();
    public int coinAmount = 0;



    public bool gameOver;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
       // DontDestroyOnLoad(this.gameObject);
        SaveSystem.Init();
    }

    private void Start()
    {
        if (carGameObject != null)
        {
            playerUnit = carGameObject.GetComponent<CarMovement>();
            UpdateCoinCount();
        }
        //saveManager = new SaveManager();
    }

    public void UpdateCoinCount()
    {
        coins.Clear();
        coins = FindObjectsOfType<CoinCollection>().ToList();
 
        coinDictionary.Clear();

        for (int i = 0; i < coins.Count; i++)
        {
            coinDictionary.Add(coins[i].coinId, coins[i].collected);
        }
    }

    private void Update()
    {
        if (!gameOver)
        {
            currentTime += Time.deltaTime;
        }


        if (carGameObject != null)
        {
            //KEY INPUTS - 
            if (Input.GetKeyDown(KeyCode.K))
            {
                Save(true);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                coinAmount++;
            }
        }
    }

    public int CalculateScore(float time, int coinAMT)
    {
        int score = ((100000 - ((int)(time * 1000))) * coinAmount) / 1000;
        Debug.Log("time says: " + (int)(time * 1000));
        Debug.Log("10000-time says: " + (100000 - ((int)(time * 1000))));
        if (score < 0)
        {
            score = 0;
        }

        return score;
    }

    public void Save(bool newSave)
    {
        SaveManager.instance.SaveGame(newSave);

    }

    public void Load()
    {
        SaveManager.instance.LoadGame(true);
       
    }

}
