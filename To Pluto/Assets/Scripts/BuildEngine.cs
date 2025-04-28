using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BuildEngine : MonoBehaviour
{
    DisplayManager displayManager;
    //[SerializeField] Image iconFillImage;
    //[SerializeField] Image upgradeIconFillImage;
    //[SerializeField] Image detectorFillImage;

    


    float timer = 0;
    bool currentlyBuilding = false;
    bool currentlyUpgrading = false;
    bool currentlyUpgradingDetector = false;
    //EngineSO engineToBuild;
    PartSO partToBuild;
    int engineUpgradeNumber;


    //[SerializeField] TMP_Text engineCountText;
    //[SerializeField] TMP_Text detectorLevelText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        displayManager = GameManager.instance.displayManager;
        displayManager.engineCountText.text = GetCurrentEngineCount() + "/" + GameManager.instance.shipController.engineCountMax;
        displayManager.detectorLevelText.text = GameManager.instance.shipController.detectorLevel.ToString();

        
    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (currentlyBuilding)
        {
            
            if (partToBuild.GetType() == typeof(EnginePartSO))
            {
                //NEED TO LOOK AT THIS *************************************************************************************
                displayManager.iconFillImage.fillAmount = (timer / partToBuild.buildUpgradeTime[0]);
            }
            else if (partToBuild.GetType() == typeof(ThrusterPartSO))
            {
               // Debug.Log("we think its a thruster");
                displayManager.thrusterResearchFillImages[engineUpgradeNumber].fillAmount = (timer / partToBuild.buildUpgradeTime[partToBuild.currentLevel + 1]);
            }
            //TractorBeam
            else if (partToBuild.GetType() == typeof(PartSO))
            {
                //Debug.Log("we think its a tractorBeam");
                displayManager.iconFillImage.fillAmount = (timer / partToBuild.buildUpgradeTime[GameManager.instance.shipController.tractorBeamCount]);
            }
        }
        else if(currentlyUpgrading)
        {
            if (partToBuild.GetType() == typeof(EnginePartSO))
            {
                //Debug.Log("we think its an engine");
                displayManager.engineResearchFillImages[engineUpgradeNumber].fillAmount = (timer / partToBuild.buildUpgradeTime[partToBuild.currentLevel + 1]);
            }
            else if (partToBuild.GetType() == typeof(ThrusterPartSO))
            {
                //Debug.Log("we think its a thruster");
                displayManager.thrusterResearchFillImages[engineUpgradeNumber].fillAmount = (timer / partToBuild.buildUpgradeTime[partToBuild.currentLevel + 1]);
            }
            //TractorBeam
            else if (partToBuild.GetType() == typeof(PartSO))
            {
                //Debug.Log("we think its a tractorBeam");
                displayManager.tractorBeamFillImage.fillAmount = (timer / partToBuild.buildUpgradeTime[GameManager.instance.shipController.tractorBeamCount]);
            }
        }     
        else if(currentlyUpgradingDetector)
        {
            //NEED TO CHANGE THIS
            displayManager.detectorFillImage.fillAmount = (timer / partToBuild.buildUpgradeTime[GameManager.instance.shipController.detectorLevel + 1]);
        }
    }

    //Get currentEngineCount
    int GetCurrentEngineCount()
    {
        int count = 0;
        for (int i = 0; i < GameManager.instance.shipController.enginePartSOList.Count; i++)
        {
            //if (GameManager.instance.shipController.enginepartArr[i].currentLevel > 0)
            {
                count++;
            }
        }
        return count;
    }

    public void AddEngine(EnginePartSO engine)
    {
        //NEED TO CHANGE THIS
        if (engine.cost[0] <= GameManager.instance.shipController.resourceCount && !currentlyBuilding && GetCurrentEngineCount() < GameManager.instance.shipController.engineCountMax && !currentlyUpgrading && !currentlyUpgradingDetector)
        {
            Debug.Log("we got resources");
            
            currentlyBuilding = true;
            StartCoroutine(BuildCoroutine(engine));
            timer = 0;
            partToBuild = engine;
            displayManager.iconFillImage = displayManager.BuildEngineFillImage;

        }
        else
        {
            Debug.Log("out of resources");
        }
        if(GetCurrentEngineCount() >= GameManager.instance.shipController.engineCountMax)
        {
            Debug.Log("Max engine reached");
        }
    }
    public void AddTractorBeam()
    {
        //NEED TO CHANGE THIS
        if (GameManager.instance.shipController.tractorPartSOReference.cost[GameManager.instance.shipController.tractorBeamCount] <= GameManager.instance.shipController.resourceCount && !currentlyBuilding && GameManager.instance.shipController.tractorBeamCount < GameManager.instance.shipController.tractorBeamCountMax && !currentlyUpgrading && !currentlyUpgradingDetector)
        {
            Debug.Log("we got resources");

            currentlyBuilding = true;
            StartCoroutine(BuildCoroutine());
            timer = 0;
            partToBuild = GameManager.instance.shipController.tractorPartSOReference;
            displayManager.iconFillImage = displayManager.tractorBeamFillImage;

        }
        else
        {
            Debug.Log("out of resources");
        }
        if (GetCurrentEngineCount() >= GameManager.instance.shipController.engineCountMax)
        {
            Debug.Log("Max engine reached");
        }
    }

    //This build creates an Engine Fuel Efficiency as well as a thruster
    IEnumerator BuildCoroutine(EnginePartSO engine)
    {
        
        Debug.Log("Building Engine and Thruster. Time to Completion: " + engine.buildUpgradeTime[0]);
        GameManager.instance.shipController.resourceCount -= engine.cost[0];
        yield return new WaitForSeconds(engine.buildUpgradeTime[0]);

        Debug.Log("Building Complete");
        
        GameManager.instance.shipController.enginePartSOList.Add(Instantiate(engine));
        GameManager.instance.shipController.thrusterPartSOList.Add(Instantiate(GameManager.instance.shipController.thrusterPartSOReference));


        displayManager.engineCountText.text = GetCurrentEngineCount() + "/" + GameManager.instance.shipController.engineCountMax;
        currentlyBuilding = false;
        displayManager.iconFillImage.fillAmount = 0;
    }
    IEnumerator BuildCoroutine()
    {
        PartSO tractorBeam = GameManager.instance.shipController.tractorPartSOReference;
        Debug.Log("Building Tractor Beam: " + tractorBeam.buildUpgradeTime[GameManager.instance.shipController.tractorBeamCount]);
        GameManager.instance.shipController.resourceCount -= GameManager.instance.shipController.tractorPartSOReference.cost[GameManager.instance.shipController.tractorBeamCount];

        yield return new WaitForSeconds(tractorBeam.buildUpgradeTime[GameManager.instance.shipController.tractorBeamCount]);                                                                                                                                                                                                                                                                                                                                                                                                                          

        Debug.Log("Building Complete");

        GameManager.instance.shipController.tractorBeamGOList[GameManager.instance.shipController.tractorBeamCount].SetActive(true);

        GameManager.instance.shipController.tractorBeamCount++;
                

        displayManager.tractorBeamCountText.text = GameManager.instance.shipController.tractorBeamCount + "/" + GameManager.instance.shipController.tractorBeamCountMax;
        currentlyBuilding = false;
        displayManager.iconFillImage.fillAmount = 0;
    }




    public void UpdateEngine(int engineNumber)
    {
        if (engineNumber < GetCurrentEngineCount() && !currentlyUpgrading)
        {
            engineUpgradeNumber = engineNumber;

            PartSO currentEngine  = GameManager.instance.shipController.enginePartSOList[engineNumber];
            
            if (currentEngine.cost[currentEngine.currentLevel + 1] <= GameManager.instance.shipController.resourceCount && !currentlyBuilding && !currentlyUpgrading && !currentlyUpgradingDetector)
            {

                        if (currentEngine.currentLevel < currentEngine.maxLevel)
                        {
                            currentlyUpgrading = true;
                            StartCoroutine(UpgradeCoroutine(currentEngine));
                            timer = 0;
                            partToBuild = currentEngine;
                            // displayManager.upgradeIconFillImage = fillImage;
                            return;
                        }
            }
        }
        else
        {
            Debug.Log("No engine here");
        }
    }
    public void UpdateThruster(int thrusterNumber)
    {
        if (thrusterNumber < GetCurrentEngineCount() && !currentlyUpgrading)
        {
            engineUpgradeNumber = thrusterNumber;

            PartSO currentEngine = GameManager.instance.shipController.thrusterPartSOList[thrusterNumber];

            if (currentEngine.cost[currentEngine.currentLevel + 1] <= GameManager.instance.shipController.resourceCount && !currentlyBuilding && !currentlyUpgrading && !currentlyUpgradingDetector)
            {

                if (currentEngine.currentLevel < currentEngine.maxLevel)
                {
                    currentlyUpgrading = true;
                    StartCoroutine(UpgradeCoroutine(currentEngine));
                    timer = 0;
                    partToBuild = currentEngine;
                    // displayManager.upgradeIconFillImage = fillImage;
                    return;
                }
            }
        }
        else
        {
            Debug.Log("No engine here");
        }
    }



    IEnumerator UpgradeCoroutine(PartSO currentEngine)
    {
        GameManager.instance.shipController.resourceCount -= currentEngine.cost[currentEngine.currentLevel+1];

        yield return new WaitForSeconds(currentEngine.buildUpgradeTime[currentEngine.currentLevel + 1]);


        currentEngine.currentLevel++;
          
        
  
        currentlyUpgrading = false;
        if (currentEngine.GetType() == typeof(EnginePartSO))
        {
            Debug.Log("type is engine");
            displayManager.engineResearchFillImages[engineUpgradeNumber].fillAmount = 0;
            displayManager.engineUpgradeTexts[engineUpgradeNumber].text = "LEVEL " + currentEngine.currentLevel;
        }
        else if (currentEngine.GetType() == typeof(ThrusterPartSO))
        {
            Debug.Log("type is thruster");
            displayManager.thrusterResearchFillImages[engineUpgradeNumber].fillAmount = 0;
            displayManager.thrusterUpgradeTexts[engineUpgradeNumber].text = "LEVEL " + currentEngine.currentLevel;
        }
    }

    public void UpgradeDetector()
    {
        /*
        EngineSO detector = GameManager.instance.shipController.detector;

        Debug.Log("clicked ");
        Debug.Log("clicked " + detector.cost[GameManager.instance.shipController.detectorLevel + 1]);
        Debug.Log("clicked " + GameManager.instance.shipController.resourceCount);

        if (detector.cost[GameManager.instance.shipController.detectorLevel +1] <= GameManager.instance.shipController.resourceCount && !currentlyBuilding && !currentlyUpgrading && !currentlyUpgradingDetector)
        {
            Debug.Log("got passed the if statement");

            currentlyUpgradingDetector = true;
            StartCoroutine(UpgradeDetectorCoroutine(detector));
            engineToBuild = detector;
            timer = 0;
        }
        */
    }

    IEnumerator UpgradeDetectorCoroutine(EngineSO detector)
    {
        GameManager.instance.shipController.resourceCount -= detector.cost[GameManager.instance.shipController.detectorLevel + 1];
        yield return new WaitForSeconds(detector.buildUpgradeTime[GameManager.instance.shipController.detectorLevel +1]);
        Debug.Log("finished!");

        GameManager.instance.shipController.detectorLevel++;
        displayManager.detectorLevelText.text = GameManager.instance.shipController.detectorLevel.ToString();
        displayManager.detectorFillImage.fillAmount = 0;
        currentlyUpgradingDetector = false;
    }

    public void UpgradeTractorBeam(PartSO tractorBeamSO)
    {

    }

  //  IEnumerator UpgradeTractorBeamCoroutine(PartSO tractorBeamSO)
  //  {
        //yield return new WaitForSeconds(tractorBeamSO.buildUpgradeTime[GameManager.instance.shipController.tractorPartSOReference]);
  //  }
}
