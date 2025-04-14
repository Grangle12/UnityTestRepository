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
    public int detectorLevel = 0;

    public List<EngineSO> engineList = new List<EngineSO>();

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

        //Make new engineSO so not to affect original
        List<EngineSO> tempEngineList = new List<EngineSO>();
        
        for(int i =0; i < engineList.Count; i++)
        {
            tempEngineList.Add(Instantiate(engineList[i]));

        }
        engineList.Clear();
        for (int i = 0; i < tempEngineList.Count; i++)
        {
            engineList.Add(tempEngineList[i]);

        }

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
            for (int i = 0; i < engineList.Count; i++)
            {
                if (fuel > 0)
                {
                    //NEED TO CHANGE THIS 
                    fuel -= (1/engineList[i].fuelEfficiency[0]);
                    speedKmps += engineList[i].acceleration[0];
                }
                else
                {
                    fuel = 0;
                }
            }

        }

    }

    
}
