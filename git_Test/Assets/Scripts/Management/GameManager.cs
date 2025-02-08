using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject carGameObject;
    public int coinAmount = 0;
    private CarMovement playerUnit;
    public float currentTime;
    public SerializableDictionary<int, bool> coinDictionary = new SerializableDictionary<int, bool>();
    List<CoinCollection> coins = new List<CoinCollection>();

    public bool gameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        SaveSystem.Init();

        playerUnit = carGameObject.GetComponent<CarMovement>();

        SaveObject saveObject = new SaveObject
        {
            saveCoinAmount = 5,
            carPosition = carGameObject.transform.position
        };
        string json = JsonUtility.ToJson(saveObject);
        Debug.Log("saved = " + json);

        SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(json);
        Debug.Log("loaded = " + loadedSaveObject.saveCoinAmount);
    }

    private void Start()
    {

        UpdateCoinCount();


    }

    void UpdateCoinCount()
    {
        coins.Clear();
        Debug.Log("coin count is: " + coins.Count);

        coins = FindObjectsOfType<CoinCollection>().ToList();
        Debug.Log("new coin count is: " + coins.Count);

        coinDictionary.Clear();

        for (int i = 0; i < coins.Count; i++)
        {
            coinDictionary.Add(coins[i].coinId, coins[i].collected);
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K pressed - Saving Game....");
            Save();
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            coinAmount++;
            Debug.Log("current gold is: " + coinAmount);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("l pressed");
            Load();
            Debug.Log("current gold is: " + coinAmount);
        }
    }

    private void Save()
    {
        UpdateCoinCount();

        SaveObject saveObject = new SaveObject
        {
            saveTime = currentTime,
            saveCoinAmount = coinAmount,
            carPosition = carGameObject.transform.position,
            carMovement = playerUnit,
            saveCoinDictionary = coinDictionary

        };
        string json = JsonUtility.ToJson(saveObject);

        SaveSystem.Save(json);
    }

    private void Load()
    {
        string saveString = SaveSystem.Load();
        if(saveString != null)
        {
            Debug.Log("Loaded: " + saveString);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            currentTime = saveObject.saveTime;
            coinAmount = saveObject.saveCoinAmount;
            this.GetComponent<DisplayText>().UpdateCoinCounter();

            carGameObject.transform.position = saveObject.carPosition;
            playerUnit = saveObject.carMovement;
            coinDictionary = saveObject.saveCoinDictionary;
            
            foreach(var coin in coins)
            {
                coin.LoadCoinData();
            }

            
        }
    }

    private class SaveObject
    {
        public float saveTime;
        
        public int saveCoinAmount;
        public Vector3 carPosition;
        public CarMovement carMovement;
        public SerializableDictionary<int, bool> saveCoinDictionary;


    }

}
