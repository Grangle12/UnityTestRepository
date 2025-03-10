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
        SortHighScore();
    }

    private void GameOverEvent_Highscore(object sender, EventManager.OnGameOverEventArges e)
    {
        Debug.Log("got here");
        coinCount = GameManager.instance.coinAmount;
        time = GameManager.instance.currentTime;
        
            
        
        ///////// Old dictionary in dictionary way
        coinTimeDictionary.Add(coinCount, time);
        highScoreDictionary.Add("Player", coinTimeDictionary);
        Debug.Log("Congratulations XXXX you have found " + coinCount + " and your time was " + time.ToString("F4"));
        SaveHighScores();
            
        ////////////////
    }

    private void SaveHighScores()
    {
        SaveHighScoreObject saveHSObject = new SaveHighScoreObject
        {
            highScoreDictionarySave = highScoreDictionary,

        };
        string json = JsonUtility.ToJson(saveHSObject);

        SaveSystem.SaveHighScore(json);
        Debug.Log("HIGH SCORES SAVED??");
        
        
    }

    //Put some default scores in so the HS isnt empty, can be deleted later
    void AddHighScores(int scoreCount, TMPro.TMP_Text playerName, int coinCount)
    {
        //new { score = scoreCount, player = playerName.text, coins = coinCount}
    }

    void SortHighScore()
    {
     //   float[] topTenScores = new float[coinTimeDictionary.Count];
     //   for (int i = 0; i < coinTimeDictionary.Count; i++)
     //   {
     //       coinTimeDictionary.Values.CopyTo(topTenScores, i);
     //       Debug.Log(topTenScores[i] + " has been added");
     //   }

        var scores = new[]
        {
          new { score = 10, player = "Dave", coins = 5 },
          new { score = 9, player = "Dave" , coins = 5 },
          new { score = 8, player = "Steve", coins = 5 },
          new { score = 7, player = "Pete" , coins = 5 },
          new { score = 8, player = "Paul" , coins = 5 },
          new { score = 4, player = "Mike" , coins = 5 }
          
        };

        var top3 = scores.OrderByDescending(x => x.score);//.GroupBy(x => x.score);//.GroupBy(x => x.score);//.Select(x => x.OrderByDescending(x => x.score))
                         //.OrderByDescending(y => y.player)
                         //.ThenBy(x => x.player)
                         //.Take(3);
   
        foreach (var i in top3)
        {
            Debug.Log(i);

        }
        

    }


    private class SaveHighScoreObject
    {
        
        public SerializableDictionary<string, SerializableDictionary<int, float>> highScoreDictionarySave = new SerializableDictionary<string, SerializableDictionary<int, float>>();
        public SerializableDictionary<string, int> playerHSSave = new SerializableDictionary<string, int>();
        public SerializableDictionary<int, int> coinHSSave = new SerializableDictionary<int, int>();
        public SerializableDictionary<float, int> timeHSSave = new SerializableDictionary<float, int>();

    }


}
