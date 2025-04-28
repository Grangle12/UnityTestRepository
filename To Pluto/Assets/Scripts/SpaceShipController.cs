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
    
    public int tractorBeamCount = 1;
    public int tractorBeamCountMax = 20;


    [Tooltip("Place 0's where there is no current Engine")]
    public int[] engineLevelsStart;

    public int detectorLevel = 0;

    public EnginePartSO enginePartSOReference;
    public ThrusterPartSO thrusterPartSOReference;
    public PartSO tractorPartSOReference;


    public List<EnginePartSO> enginePartSOList = new List<EnginePartSO>();
    public List<ThrusterPartSO> thrusterPartSOList = new List<ThrusterPartSO>();

    public List<GameObject> tractorBeamGOList = new List<GameObject>();

    float timer;

    [SerializeField] private GameObject floatingTextPrefab;


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

            for (int i = 0; i < enginePartSOList.Count; i++)
            {
                if (fuel > 0)
                {
                    //NEED TO CHANGE THIS 
                    if (i < enginePartSOList.Count)
                        fuel -= (1 / enginePartSOList[i].fuelEfficiency[enginePartSOList[i].currentLevel]); //.GetValue()); ;
                    if(i < thrusterPartSOList.Count)
                        speedKmps += thrusterPartSOList[i].acceleration[thrusterPartSOList[i].currentLevel];
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
        List<EnginePartSO> tempList = enginePartSOList;
        List<ThrusterPartSO> tempThrusterList = thrusterPartSOList;
        
        for(int i =0; i < tempList.Count; i++)
        {
            enginePartSOList[i] = Instantiate(tempList[i]);
        }
        
        for (int i = 0; i < tempThrusterList.Count; i++)
        {
            thrusterPartSOList[i] = Instantiate(tempThrusterList[i]);
        }
        



    }
    
    public void ShowFloatingText(string text, Color color)
    {
        if(floatingTextPrefab)
        {
            float randX = Random.Range(-0.5f, 0.5f);
            float randY = Random.Range(-0.5f, 0.5f);
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position + (new Vector3(randX, randY, 0)), Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
            prefab.GetComponentInChildren<TextMesh>().color = color;
        }
    }
}
