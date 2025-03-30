using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HighScore : MonoBehaviour
{
    public static HighScore instance;
    EventManager gameOverEvent;
    int coinCount;
    float time;

    public SerializableDictionary<string, SerializableDictionary<int, float>> highScoreDictionary = new SerializableDictionary<string, SerializableDictionary<int, float>>();
    public SerializableDictionary<int, float> coinTimeDictionary = new SerializableDictionary<int, float>();


    List<string> playerNamesList = new List<string>();
    List<int> playerCoinCountList = new List<int>();
    List<float> playerTimeList = new List<float>();




    public TMPro.TMP_Text HSTEXT;
    public TMPro.TMP_Text playerName;

    HS_LineInfo[] highScoreArr = new HS_LineInfo[10];

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        gameOverEvent = FindObjectOfType<EventManager>().GetComponent<EventManager>();
        gameOverEvent.OnGameOver += GameOverEvent_Highscore;
        

        CreateHSList();
        SortHighScore();
    }
    void CreateHSList()
    {
        //TEST DELETE LATER
        highScoreArr[0] = new HS_LineInfo();
        highScoreArr[0].name = "Bob";
        highScoreArr[0].coinCount = 12;
        highScoreArr[0].time = 22.45f;
        highScoreArr[0].score = 1034;

        highScoreArr[1] = new HS_LineInfo();
        highScoreArr[1].name = "Greg";
        highScoreArr[1].coinCount = 5;
        highScoreArr[1].time = 19.6f;
        highScoreArr[1].score = 2109;

        for (int i = 2; i < highScoreArr.Length; i++)
        {
            highScoreArr[i] = new HS_LineInfo();
            highScoreArr[i].name = "N/a";
            highScoreArr[i].coinCount = 0;
            highScoreArr[i].time = 0.00f;
            highScoreArr[i].score = 0;
        }

        string loadingHS = SaveSystem.LoadHighScores();

        string[] splitTest = loadingHS.Split("\n");
        for(int i =0; i < splitTest.Length; i++)
        {
            if(highScoreArr[i] == null)
            {
                highScoreArr[i] = new HS_LineInfo();
                highScoreArr[i].name = "N/a";
                highScoreArr[i].coinCount = 0;
                highScoreArr[i].time = 0.00f;
                highScoreArr[i].score = 0;
            }
            highScoreArr[i] = JsonUtility.FromJson <HS_LineInfo>(splitTest[i]);
            Debug.Log("HighScore[" + i +"]" + highScoreArr[i].name);
            Debug.Log("HighScore[" + i +"]" + highScoreArr[i].score);
            Debug.Log("HighScore[" + i +"]" + highScoreArr[i].coinCount);
            Debug.Log("HighScore[" + i +"]" + highScoreArr[i].time);
            
        }
    }
    private void GameOverEvent_Highscore(object sender, EventManager.OnGameOverEventArges e)
    {
        Debug.Log("got here");
        coinCount = GameManager.instance.coinAmount;
        time = GameManager.instance.currentTime;
        
                 
        ///////// Old dictionary in dictionary way
       // coinTimeDictionary.Add(coinCount, time);
        //highScoreDictionary.Add("Player", coinTimeDictionary);
        Debug.Log("Congratulations XXXX you have found " + coinCount + " and your time was " + time.ToString("F4"));
        SaveHighScores();
            
        ////////////////
    }

    private void SaveHighScores()
    {
        /*SaveHighScoreObject saveHSObject = new SaveHighScoreObject
        {
            //highScoreDictionarySave = highScoreDictionary,

        };
        */
        //string[] strToJSONArr = new string[highScoreArr.Length];
        string combinedString = "";

        for (int i = 0; i < highScoreArr.Length; i++)
        {
            if (i == 0)
            {
                combinedString = JsonUtility.ToJson(highScoreArr[i]);
            }
            else
            {
                combinedString += "\n" + JsonUtility.ToJson(highScoreArr[i]);
            }
            
             
        }
                 

        SaveSystem.SaveHighScores(combinedString);
        Debug.Log("HIGH SCORES SAVED??");
        
        
    }

    //Put some default scores in so the HS isnt empty, can be deleted later
    void AddHighScores(int scoreCount, TMPro.TMP_Text playerName, int coinCount)
    {
        //new { score = scoreCount, player = playerName.text, coins = coinCount}
    }

    void SortHighScore()
    {
        Debug.Log("here is the array: " + highScoreArr.Length);
        highScoreArr = highScoreArr.OrderBy(x => -x.score).ToArray();

    }


    private class SaveHighScoreObject
    {
        /*
        public SerializableDictionary<string, SerializableDictionary<int, float>> highScoreDictionarySave = new SerializableDictionary<string, SerializableDictionary<int, float>>();
        public SerializableDictionary<string, int> playerHSSave = new SerializableDictionary<string, int>();
        public SerializableDictionary<int, int> coinHSSave = new SerializableDictionary<int, int>();
        public SerializableDictionary<float, int> timeHSSave = new SerializableDictionary<float, int>();
        */
        

    }


}
