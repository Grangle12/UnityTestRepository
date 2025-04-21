using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceShipController : MonoBehaviour
{


    public double playerStartPosition;
    public double playerPosition;

    float playerPosChange = 0;
       
    public float speedKmps = 10000; //*10
    
    public float fuel = 0;
    public float maxFuel = 50;

    public int resourceCount = 10;
    public int maxResourceCount = 100;

    //ENGINE Scriptable Objects;
    public int engineCountMax = 5;

    public EngineSO detector;
    public EngineSO baseEngine;
    public EnginePartStats baseEngineReference;

    [Tooltip("Place 0's where there is no current Engine")]
    public int[] engineLevelsStart;

    public int detectorLevel = 0;

    //[HideInInspector] 
    //public List<EngineSO> engineList = new List<EngineSO>();
    public EnginePartStats [] enginepartArr = new EnginePartStats[5];

    float timer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        //starting point is earth
        for (int i = 0; i < GameManager.instance.checkPointList.Count; i++)
        {
            if (GameManager.instance.checkPointList[i].checkPointName == "Earth")
            {
                playerStartPosition = GameManager.instance.checkPointList[i].distanceFromSun;
                playerPosition = GameManager.instance.checkPointList[i].distanceFromSun;
            }

        }
        InstantiateStartingEngineParts();

       // EngineSO tempEngine = baseEngine;
       // baseEngine = Instantiate(tempEngine);

        EnginePartStats tempEnginePart = baseEngineReference;
        baseEngineReference = Instantiate(tempEnginePart);

        //engineLevelsStart = new int[engineCountMax];

        /*
        engineList.Clear();
        for (int i = 0; i < engineLevelsStart.Length; i++)
        {
            if (engineLevelsStart[i] != 0)
            {
                engineList.Add(Instantiate(baseEngine));
            }

        }
        */

           

        detectorLevel = detector.currentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        playerPosChange += (speedKmps * Time.deltaTime) / 3600;

        if (playerPosChange > 1)
        {
            playerPosition += (long)playerPosChange;
            playerPosChange = 0;
        }

        if (timer > 1)
        {
         
            timer = 0;

            //Burn Fuel and Accelerate
            //for (int i = 0; i < engineList.Count; i++)
            for (int i = 0; i < enginepartArr.Length; i++)
            {
                if (fuel > 0)
                {
                    //NEED TO CHANGE THIS 
                    fuel -= (1/enginepartArr[i].fuelEfficiency.GetValue());
                    speedKmps += enginepartArr[i].acceleration.GetValue();
                }
                else
                {
                    fuel = 0;
                }
            }

        }

    }

    void InstantiateStartingEngineParts()
    {
        EnginePartStats[] tempArr = enginepartArr;
        
        for(int i =0; i < tempArr.Length; i++)
        {
            enginepartArr[i] = Instantiate(tempArr[i]);
        }
    }
    
}
