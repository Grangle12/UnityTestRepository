using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HighScore : MonoBehaviour
{
    public static HighScore instance;
    
    EventManager gameOverEvent;
    [SerializeField] MenuManager menuManager;
    [SerializeField] GameObject inputFieldGO;
    [SerializeField] GameObject textFieldGO;
    
    int coinCount;
    float time;

    public TMPro.TMP_Text HSTextName;
    public TMPro.TMP_Text HSTextTime;
    public TMPro.TMP_Text HSTextCoin;
    public TMPro.TMP_Text HSTextScore;
    public TMPro.TMP_Text HSYourScore;

    HS_LineInfo[] highScoreArr = new HS_LineInfo[10];
    string hSInput;

    int score = 0;

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
           
        }
    }
    private void GameOverEvent_Highscore(object sender, EventManager.OnGameOverEventArges e)
    {
        coinCount = GameManager.instance.coinAmount;
        time = GameManager.instance.currentTime;
        Debug.Log("Congratulations XXXX you have found " + coinCount + " and your time was " + time.ToString("F4"));
        score = GameManager.instance.CalculateScore(time, coinCount);

        if (score > highScoreArr[highScoreArr.Length - 1].score)
        {
            inputFieldGO.SetActive(true);
            textFieldGO.SetActive(true);
        }
        else
        {
            ShowHighScores();
        }
    }

    private void SaveHighScores()
    {
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
    }

    void SortHighScore()
    {
        highScoreArr = highScoreArr.OrderBy(x => -x.score).ToArray();
    }

    // This is tied to the field input of the High Score Screen that shows up when the player has a score greater than the lowest score in the top 10
    public void ReadStringInput(string s)
    {
        
        hSInput = s;
        Debug.Log(hSInput);
        
        HS_LineInfo newHS = new HS_LineInfo();
        if(hSInput.Length > 10)
        {
            hSInput = hSInput.Substring(0,10);
            Debug.Log("Player Name is Greater than 10 characters");
        }
        newHS.name = hSInput;
        newHS.coinCount = coinCount;
        newHS.time = time;
        newHS.score = score;
        highScoreArr[highScoreArr.Length - 1] = newHS;
        SortHighScore();
        SaveHighScores();

        ShowHighScores();
    }

    public void ShowHighScores()
    {

        menuManager.gameOverMenuCanvasGO.SetActive(false);
        menuManager.mainMenuCanvasGO.SetActive(false);
        menuManager.highScoreCanvasGO.SetActive(true);
        HSYourScore.text = "Your Score was: " + score.ToString();
        PopulateHSScreen();
    }
    private void PopulateHSScreen()
    {
        // This portion sets the initial text
        HSTextName.text = highScoreArr[0].name + "\n";
        HSTextTime.text = highScoreArr[0].time.ToString("F2") + "\n";
        HSTextCoin.text = highScoreArr[0].coinCount.ToString() + "\n";
        HSTextScore.text = highScoreArr[0].score + "\n";

        //This portion adds to the initial text
        for (int i = 1; i < highScoreArr.Length; i++)
        {
            HSTextName.text += highScoreArr[i].name + "\n";
            HSTextTime.text += highScoreArr[i].time.ToString("F2") + "\n";
            HSTextCoin.text += highScoreArr[i].coinCount.ToString() + "\n";
            HSTextScore.text += highScoreArr[i].score + "\n";
        }
    }
    
}
