using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ResearchManager : MonoBehaviour
{
    [SerializeField] List<Button> buttonList = new List<Button>();
    [SerializeField] List<PartSO> partList = new List<PartSO>();
    [SerializeField] List<PartStats> upgradeList = new List<PartStats>();
 
    //[SerializeField] List<bool> upgradeList = new List<bool>();

    //[SerializeField] GameObject researchPanel;

    DisplayManager displayManager;

    List<int> resrchList = new List<int>();

    float resrchSpeed = 1.5f;
    float resrchtime = 5f;

    float timer = 0;

    bool updateButton1 = false;
    bool globalRsrchUpdate = false;
    bool alreadyPopulated = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        displayManager = GameManager.instance.displayManager;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (globalRsrchUpdate)
        {
            
            //displayManager.researchButtons[0].transform.Find("FillImage").GetComponent<Image>().fillAmount = (timer / resrchtime);

            UpdateFillAmount(displayManager.researchButtons[0].transform.Find("FillImage").GetComponent<Image>(), resrchtime);
        }
    }

    public void SetGlobalResrchUpdate(bool setReserchUpdate)
    {
        globalRsrchUpdate = setReserchUpdate;
    }

    public bool GetGlobalResrchUpdate()
    {
        return globalRsrchUpdate;
    }

    void UpdateFillAmount(Image image, float rsrchTime)
    {
        image.fillAmount = timer / rsrchTime;
    }
    //This chooses 3 random researches to research
    public void PopulateResearch()
    {
        if (!alreadyPopulated)
        {
            buttonList[0].onClick.AddListener(() => UpgradePartSO(1));
            for (int i = 0; i < buttonList.Count; i++)
            {

                // researchPanel.SetActive(!researchPanel.activeSelf);
                
                //buttonList[1].onClick.AddListener(ResearchEngineEfficiency);
                //buttonList[2].onClick.AddListener(ResearchProbeLevel);
            }
        }

        
    }

    public void UnPopulateResearch()
    {
        for(int i =0; i < buttonList.Count; i++)
        {
            buttonList[i].onClick.RemoveAllListeners();
        }
    }

    public void UpgradePartSO(int resrchNum)
    {
        Debug.Log("Got to UpgradePartSO, the number is: " + resrchNum);
        //Check if any Other research is going, separating local and global if we want to have multiple research happening at somepoint
        if (!globalRsrchUpdate)
        {
            globalRsrchUpdate = true;


            //research engineFuelEfficiency
            if (resrchNum == 1)
            {
                timer = 0;

                //put in coroutine
                if (GameManager.instance.shipController.baseEngineReference.currentFuelEfficiencyLevel < GameManager.instance.shipController.baseEngineReference.maxFuelEfficiencyResearchLevel)
                {
                    resrchtime = GameManager.instance.shipController.baseEngineReference.buildUpgradeTime[GameManager.instance.shipController.baseEngineReference.fuelEfficiency.GetCurrentLevel()];

                    StartCoroutine(ResearchCoroutine(GameManager.instance.shipController.baseEngineReference, GameManager.instance.shipController.baseEngineReference.fuelEfficiency));
                    
                }

            }
            else
            {
                globalRsrchUpdate = false;
            }
        }

    }
    IEnumerator ResearchCoroutine(PartStats part, Stat stat)
    {
        int timeSlot = part.currentLevel;

        if (timeSlot < part.buildUpgradeTime.Length)
        {
            yield return new WaitForSeconds(part.buildUpgradeTime[timeSlot]);

            stat.LevelUp();
            globalRsrchUpdate = false;
            Debug.Log("Research of " + part + " with stat: " + stat + " is complete.");
        }
        else
        {
            Debug.LogWarning("There are not enough upgrade times in the array to match the current level!");
        }

       
    }
    /*
    // OLDER STUFF
    public void ResearchEngineLevel()
    {
        timer = 0;
        
        Debug.Log("Researching Engine level");
        if(!updateButton1)
        StartCoroutine(ResearchCoroutine("engineLevel", resrchEngineLevel));
        updateButton1 = true;
        //researchPanel.SetActive(false);
        //UnPopulateResearch();
    }

    public void ResearchEngineEfficiency()
    {
        timer = 0;
        Debug.Log("Researching Engine Efficiency level");
        StartCoroutine(ResearchCoroutine("engineEfficiency", resrchEngineEfficiency));
        //researchPanel.SetActive(false);
        //UnPopulateResearch();
    }

    public void ResearchDetectorLevel()
    {
        timer = 0;
        Debug.Log("Researching Detector level");
        StartCoroutine(ResearchCoroutine("detector", resrchDetectorLevel));
        //researchPanel.SetActive(false);
        UnPopulateResearch();
    }
    public void ResearchProbeLevel()
    {
        timer = 0;
        Debug.Log("Researching Probe level");
        StartCoroutine(ResearchCoroutine("probe",resrchProbeLevel));
        //researchPanel.SetActive(false);
        UnPopulateResearch();
    }

    IEnumerator ResearchCoroutine(string researchType, int research)
    {
        
        yield return new WaitForSeconds(resrchtime);
        
        if(researchType == "engineLevel")
        {
            if(GameManager.instance.shipController.baseEngine.currentResearchLevelFE < GameManager.instance.shipController.baseEngine.maxLevel)
            {
                GameManager.instance.shipController.baseEngine.currentResearchLevelFE++;
                for(int i =0; i < GameManager.instance.shipController.engineList.Count; i++ )
                {
                    GameManager.instance.shipController.engineList[i].currentResearchLevelFE++;
                    
                }
            if(GameManager.instance.shipController.baseEngineReference.currentFuelEfficiencyResearchLevel < GameManager.instance.shipController.baseEngineReference.maxFuelEfficiencyResearchLevel)
                for (int i = 0; i < GameManager.instance.shipController.enginepartArr.Length; i++)
                {
                    GameManager.instance.shipController.enginepartArr[i].currentFuelEfficiencyResearchLevel++;
                    //Debug.Log("Modified Fuel Eff is: " + GameManager.instance.shipController.enginepartArr[i].fuelEfficiency.GetValue());
                }
            }
            else
            {
                Debug.LogWarning("TRYING TO UPGRADE PAST MAX LEVEL");
            }
            updateButton1 = false;
            displayManager.researchButtons[0].transform.Find("FillImage").GetComponent<Image>().fillAmount = 0;
            //resrchEngineLevel++;
        }
        else if (researchType == "engineEfficiency")
        {
            resrchEngineEfficiency++;
        }
        else if (researchType == "detector")
        {
            resrchDetectorLevel++;
        }
        else if (researchType == "probe")
        {
            resrchProbeLevel++;
        }

        Debug.Log("Research finished!!!");
    }
    */
}
