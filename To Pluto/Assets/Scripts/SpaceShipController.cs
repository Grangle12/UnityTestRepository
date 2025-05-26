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
    public PartSO detectorPartSOReference;


    public List<EnginePartSO> enginePartSOList = new List<EnginePartSO>();
    public List<ThrusterPartSO> thrusterPartSOList = new List<ThrusterPartSO>();

    public List<GameObject> tractorBeamGOList = new List<GameObject>();

    float timer;

    [SerializeField] private GameObject floatingTextPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enginePartSOReference = Instantiate(enginePartSOReference);
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
            int totalSpeedBoost = 0;

            for (int i = 0; i < enginePartSOList.Count; i++)
            {
                if (fuel > 1 / enginePartSOList[i].fuelEfficiency[enginePartSOList[i].currentLevel])
                {
                    //NEED TO CHANGE THIS 
                    if (i < enginePartSOList.Count)
                        fuel -= (1 / enginePartSOList[i].fuelEfficiency[enginePartSOList[i].currentLevel]); //.GetValue()); ;
                    if (i < thrusterPartSOList.Count)
                    {
                        speedKmps += thrusterPartSOList[i].acceleration[thrusterPartSOList[i].currentLevel];
                        totalSpeedBoost += thrusterPartSOList[i].acceleration[thrusterPartSOList[i].currentLevel];
                        //GameManager.instance.displayManager.CreateFloatingText(GameManager.instance.displayManager.playerSpeed_Text.transform, "+" + thrusterPartSOList[i].acceleration[thrusterPartSOList[i].currentLevel].ToString());
                    }
                }
                else
                {
                    fuel = 0;
                }
            }
            if (totalSpeedBoost > 0)
            {
                GameManager.instance.displayManager.CreateFloatingText(GameManager.instance.displayManager.playerSpeed_Text.transform, "+" + totalSpeedBoost);
                
            }
        }

    }

    void InstantiateStartingEngineParts()
    {
        List<EnginePartSO> tempList = enginePartSOList;
        List<ThrusterPartSO> tempThrusterList = thrusterPartSOList;
        PartSO tempTractor = tractorPartSOReference;
        PartSO tempDetector = detectorPartSOReference;


        for(int i =0; i < tempList.Count; i++)
        {
            enginePartSOList[i] = Instantiate(tempList[i]);
        }
        
        for (int i = 0; i < tempThrusterList.Count; i++)
        {
            thrusterPartSOList[i] = Instantiate(tempThrusterList[i]);
        }

        tractorPartSOReference = Instantiate(tempTractor);
        detectorPartSOReference = Instantiate(tempDetector);



    }
    
    public void ShowFloatingText(string text, Color color, string typeOfResource)
    {
        if(floatingTextPrefab)
        {
            
            float randX = Random.Range(-0.5f, 0.5f);
            float randY = Random.Range(-0.5f, 0.5f);
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position + (new Vector3(randX, randY, 0)), Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
            prefab.GetComponentInChildren<TextMesh>().color = color;


            Transform childOfPrefab = prefab.transform.Find("FloatingText");


            if(typeOfResource == "Energy")
            {
                childOfPrefab.Find("Energy_0").gameObject.SetActive(true);
                childOfPrefab.Find("gem2_green_0").gameObject.SetActive(false);
                Debug.Log("I AM SUPPOSED TO BE WHITE");
            }
            else if (typeOfResource == "Gem1")
            {
                childOfPrefab.Find("gem2_green_0").gameObject.SetActive(true);
                Debug.Log("I found" + childOfPrefab.Find("gem2_green_0").gameObject);

                childOfPrefab.Find("Energy_0").gameObject.SetActive(false);
                Debug.Log("I found" + childOfPrefab.Find("Energy_0").gameObject);
                Debug.Log("I AM SUPPOSED TO BE BLUE");

            }
        }
    }
}
