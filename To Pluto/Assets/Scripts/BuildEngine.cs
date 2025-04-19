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
    EngineSO engineToBuild;
    int engineUpgradeNumber;


    //[SerializeField] TMP_Text engineCountText;
    //[SerializeField] TMP_Text detectorLevelText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        displayManager = GameManager.instance.displayManager;
        displayManager.engineCountText.text = GameManager.instance.shipController.engineList.Count.ToString() + "/" + GameManager.instance.shipController.engineCountMax;
        displayManager.detectorLevelText.text = GameManager.instance.shipController.detectorLevel.ToString();

        
    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (currentlyBuilding)
        {
            //NEED TO CHANGE THIS
            displayManager.iconFillImage.fillAmount = (timer / engineToBuild.buildUpgradeTime[0]);
        }
        else if(currentlyUpgrading)
        {
            
            displayManager.engineResearchFillImages[engineUpgradeNumber].fillAmount = (timer / engineToBuild.buildUpgradeTime[engineToBuild.currentLevel+1]);
        }     
        else if(currentlyUpgradingDetector)
        {
            //NEED TO CHANGE THIS
            displayManager.detectorFillImage.fillAmount = (timer / engineToBuild.buildUpgradeTime[GameManager.instance.shipController.detectorLevel + 1]);
        }
    }

    public void AddEngine(EngineSO engine)
    {
        //NEED TO CHANGE THIS
        if (engine.cost[0] <= GameManager.instance.shipController.resourceCount && !currentlyBuilding && GameManager.instance.shipController.engineList.Count < GameManager.instance.shipController.engineCountMax && !currentlyUpgrading && !currentlyUpgradingDetector)
        {
            Debug.Log("we got resources");
            
            currentlyBuilding = true;
            StartCoroutine(BuildCoroutine(engine));
            timer = 0;
            engineToBuild = engine;


        }
        else
        {
            Debug.Log("out of resources");
        }
        if(GameManager.instance.shipController.engineList.Count >= GameManager.instance.shipController.engineCountMax)
        {
            Debug.Log("Max engine reached");
        }
    }

    IEnumerator BuildCoroutine(EngineSO engine)
    {
        
        Debug.Log("coroutine Started be back in: " + engine.buildUpgradeTime[0]);
        yield return new WaitForSeconds(engine.buildUpgradeTime[0]);

        Debug.Log("coroutine ended");
        GameManager.instance.shipController.engineList.Add(Instantiate(engine));
        GameManager.instance.shipController.resourceCount -= engine.cost[0];

        displayManager.engineCountText.text = GameManager.instance.shipController.engineList.Count.ToString() + "/" + GameManager.instance.shipController.engineCountMax;
        currentlyBuilding = false;
        displayManager.iconFillImage.fillAmount = 0;
    }

    public void UpdateEngine(int engineNumber)
    {
        if (engineNumber < GameManager.instance.shipController.engineList.Count && !currentlyUpgrading)
        {
            engineUpgradeNumber = engineNumber;
            EngineSO currentEngine = GameManager.instance.shipController.engineList[engineNumber];
            
            if (currentEngine.cost[currentEngine.currentLevel + 1] <= GameManager.instance.shipController.resourceCount && !currentlyBuilding && !currentlyUpgrading && !currentlyUpgradingDetector)
            {
                
                //Check lowest level engine
                // for (int i = 0; i < GameManager.instance.shipController.engineList.Count; i++)
                {
                    // if (GameManager.instance.shipController.engineList[i].currentLevel == 1)

                    {
                        if (currentEngine.currentLevel < currentEngine.currentResearchLevelFE)
                        {
                            if (currentEngine.currentLevel < currentEngine.maxLevel)
                            {
                                currentlyUpgrading = true;
                                StartCoroutine(UpgradeCoroutine(currentEngine));
                                timer = 0;
                                engineToBuild = currentEngine;
                               // displayManager.upgradeIconFillImage = fillImage;
                                return;
                            }
                        }
                        else
                        {
                            Debug.Log("Need to research to a higher level");
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("No engine here");
        }
    }
    IEnumerator UpgradeCoroutine(EngineSO currentEngine)
    {
        GameManager.instance.shipController.resourceCount -= currentEngine.cost[currentEngine.currentLevel+1];

        yield return new WaitForSeconds(currentEngine.buildUpgradeTime[currentEngine.currentLevel + 1]);


        currentEngine.currentLevel++;
          
        
  
        currentlyUpgrading = false;
        displayManager.engineResearchFillImages[engineUpgradeNumber].fillAmount = 0;
        displayManager.engineUpgradeTexts[engineUpgradeNumber].text = "LEVEL " + currentEngine.currentLevel;
    }

    public void UpgradeDetector()
    {
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
}
