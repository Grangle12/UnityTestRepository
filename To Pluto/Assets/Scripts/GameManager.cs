using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public DisplayManager displayManager;
    public ResearchManager researchManager;

    public SpaceShipController shipController;


    public float currentTime = 0;
    
    
    public int asteroidClickCounter;
    public int plutoId;

    public List<CheckPointSO> checkPointList = new List<CheckPointSO>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        displayManager = this.GetComponent<DisplayManager>();
        SortCheckPointList();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        UpdatePositionInformation();

    }
    void UpdatePositionInformation()
    {
        
        DistanceToNextCheckPoint();


        TimeToCheckpoint(displayManager.timeToPluto_Text, checkPointList[plutoId]);

    }
    
    private void SortCheckPointList()
    {
        List<CheckPointSO> tempCheckPointList = checkPointList.OrderBy(x => x.distanceFromSun).ToList();
        
        checkPointList = tempCheckPointList;
        for (int i = 0; i < checkPointList.Count; i++)
        {
            if (checkPointList[i].distanceFromSun < shipController.playerPosition)
            {
                GameManager.instance.checkPointList[i].hasArrived = true;
            }
            else
            {
                checkPointList[i].hasArrived = false;
            }

            if (checkPointList[i].checkPointName == "Pluto")
            {
                plutoId = i;
            }
        }
    }
    
    
    private void DistanceToNextCheckPoint()
    {
        for(int i = 0; i < checkPointList.Count; i++)
        {
           
            
            if (checkPointList[i].distanceFromSun > shipController.playerPosition)
            {
    
                displayManager.distToNextChkPnt_Text.text = (checkPointList[i].distanceFromSun - shipController.playerPosition).ToString("f0")  + " Remaining distance to " + checkPointList[i].checkPointName;
                
                TimeToCheckpoint(displayManager.timeToNextChkPnt_Text,checkPointList[i]);
                return;
            }
            else
            {
                //Check if Reached Checkpoint for first time
                if(!checkPointList[i].hasArrived)
                {
                    Debug.Log("CONGRATULATIONS ON MAKING IT TO " + checkPointList[i].checkPointName);
                    checkPointList[i].hasArrived = true;
                }
            }
        }
    }

    private void TimeToCheckpoint(TMP_Text textBox, CheckPointSO checkPointSO)
    {
        double timeToNextCheckpoint = (checkPointSO.distanceFromSun - shipController.playerPosition) / shipController.speedKmps;

        // Shows time in years
        if (timeToNextCheckpoint / 24 / 365 > 1)
        {

            textBox.text = (timeToNextCheckpoint / 24 / 365).ToString("f2") + " years remaining to " + checkPointSO.checkPointName;
        }
        //shows time in days
        else if (timeToNextCheckpoint / 24 > 1)
        {
            textBox.text = ((timeToNextCheckpoint) / 24).ToString("f2") + " days remaining to " + checkPointSO.checkPointName;
        }
        else if (timeToNextCheckpoint > 1)
        {
          
            textBox.text = (timeToNextCheckpoint).ToString("f2") + " hours remaining to " + checkPointSO.checkPointName;
        }
        else if (timeToNextCheckpoint*60 > 1)
        {
            
                textBox.text = (timeToNextCheckpoint * 60).ToString("f2") + " minutes remaining to " + checkPointSO.checkPointName;
            
        }
        else
        {
            textBox.text = (timeToNextCheckpoint * 60 * 60).ToString("f1") + " seconds remaining to" + checkPointSO.checkPointName;
        }
    }

}
