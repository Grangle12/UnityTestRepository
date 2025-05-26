using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;


public class ResearchManager : MonoBehaviour
{
    DisplayManager displayManager;

    [SerializeField] List<Button> buttonList = new List<Button>();

    [SerializeField] List<PartSO> listOfResearchableParts;
    
    float resrchtime = 5f;

    float timer = 0;

    bool researching = false;
    bool alreadyPopulated = false;

    PartSO research1, research2, research3;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        displayManager = GameManager.instance.displayManager;

        RecreateListOfResearchableParts();
        
    }

  

    // Update is called once per frame
    void Update()
    {
        
        if (researching)
        {
            timer += Time.deltaTime;
            //displayManager.researchButtons[0].transform.Find("FillImage").GetComponent<Image>().fillAmount = (timer / resrchtime);

            UpdateFillAmount(displayManager.upgradeButtonFillImage, timer/resrchtime);
        }
        
    }
    void RecreateListOfResearchableParts()
    {
        List<PartSO> TempList1 = new List<PartSO>();

        for (int i = 0; i < listOfResearchableParts.Count; i++)
        {
            TempList1.Add(listOfResearchableParts[i]);


        }

        listOfResearchableParts.Clear();

        for (int i = 0; i < TempList1.Count; i++)
        {
            listOfResearchableParts.Add(Instantiate(TempList1[i]));

        }
    }
    void UpdateFillAmount(Image image, float rsrchTime)
    {
        image.fillAmount = rsrchTime;
    }

    //This chooses 3 random researches to research
    public void PopulateResearch()
    {
        int a;

        List<PartSO> TempList1 = new List<PartSO>();

        //Debug.Log(listOfResearchableParts.Count + " is researchableparts count");
        //Only show research that is below max level
        for(int i = 0; i < listOfResearchableParts.Count; i++)
        {
            if(listOfResearchableParts[i].currentLevel < listOfResearchableParts[i].maxLevel)
            {
                TempList1.Add(listOfResearchableParts[i]);
               // Debug.Log("Part Added: " + listOfResearchableParts[i].partName);
            }
        }
        //Debug.Log(TempList1.Count + " is templist count");

        //Assing research randomly to buttons
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (TempList1.Count > 0)
            {
                a = Random.Range(0, TempList1.Count);
                for (int y = 0; y < listOfResearchableParts.Count; y++)
                {

                    if (listOfResearchableParts[y].partName == TempList1[a].partName)
                    {
                        //Debug.Log("were saying " + listOfResearchableParts[y].partName + " is equal to " + TempList1[a].partName + " and i, y =" + i+ ", " + y);

                        buttonList[i].onClick.AddListener(delegate { AssignFillImage(EventSystem.current.currentSelectedGameObject); });
                        buttonList[i].onClick.AddListener(() => UpgradePartSO(listOfResearchableParts[y]));
                        buttonList[i].transform.gameObject.GetComponent<Image>().sprite = listOfResearchableParts[y].icon;

                        TempList1.Remove(TempList1[a]);
                      //  Debug.Log(TempList1.Count + " is templist count");
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Out of Research to assign!");
            }
        }
    }

    public void AssignFillImage(GameObject gameObject)
    {

        if (!researching)
        {
            Debug.Log("Got here, object is:" + gameObject);
            displayManager.upgradeButtonFillImage = gameObject.transform.Find("FillImage").GetComponent<Image>();
        }
    }

    public void UnPopulateResearch()
    {
        for(int i =0; i < buttonList.Count; i++)
        {
            buttonList[i].onClick.RemoveAllListeners();
        }
    }

    public void UpgradePartSO(PartSO part)
    {
        //PartSO gMPart =
        //ScriptableObject.CreateInstance(PartSO part);

        if (!researching)
        {
            for(int i =0; i < listOfResearchableParts.Count; i++)
            {
                if( part.partName == listOfResearchableParts[i].partName)
                {
                    part = listOfResearchableParts[i];
                    Debug.Log(part.partName + " is attempted to being researched");
                }
            }
            if (part  != null)
            {
                if (GameManager.instance.shipController.resourceCount >= part.cost[part.currentLevel])
                {
                    Debug.Log("researching: " + part.partName);
                    GameManager.instance.shipController.resourceCount -= part.cost[part.currentLevel];
                    researching = true;
                    StartCoroutine(ResearchCoroutine(part));
                }
                else
                {
                    Debug.Log("unable to research, the cost of " + part.partName + " is: " + part.cost[part.currentLevel]);
                }
            }
            else
            {
                Debug.Log("No matching Part in the research List");
            }
        }
        else
        {
            Debug.Log("Currently Researching another project");
        }
    }
    


    IEnumerator ResearchCoroutine(PartSO part)
    {
        //use of current level because level starts at 1 and not at 0
        resrchtime = part.buildUpgradeTime[part.maxLevel];
        Debug.Log("please wait..." + part.buildUpgradeTime[part.maxLevel]);
        yield return new WaitForSeconds(part.buildUpgradeTime[part.maxLevel]);

        part.maxLevel++;
        SetMaxLevelOfPart(part);

        timer = 0;
        UpdateFillAmount(displayManager.upgradeButtonFillImage, timer / 0);
        researching = false;
        Debug.Log("Research of " + part + " is complete.");
    }
    public void SetMaxLevelOfPart(PartSO part)
    {
        if (part.GetType() == typeof(EnginePartSO))
        {
            Debug.Log("Were an engine");
            for (int i = 0; i < GameManager.instance.shipController.enginePartSOList.Count; i++)
            {
                GameManager.instance.shipController.enginePartSOList[i].maxLevel = part.maxLevel;
            }
        }
        else if (part.GetType() == typeof(ThrusterPartSO))
        {
            Debug.Log("Were a thruster");
            for (int i = 0; i < GameManager.instance.shipController.thrusterPartSOList.Count; i++)
            {
                GameManager.instance.shipController.thrusterPartSOList[i].maxLevel = part.maxLevel;
            }
        }
        else if (part.partName == "Tractor Beam")
        {
            Debug.Log("Were a TractorBeam");
            //for (int i = 0; i < GameManager.instance.shipController.thrusterPartSOList.Count; i++)
            {
                GameManager.instance.shipController.tractorBeamCountMax = part.maxLevel;
            }
        }
        else if (part.partName == "Detector")
        {
            Debug.Log("Were a detector");
            //for (int i = 0; i < GameManager.instance.shipController.; i++)
            {
                GameManager.instance.shipController.detectorPartSOReference.maxLevel = part.maxLevel;
            }
        }
    }

    public int GetPartCost(string partName)
    {
        for (int i = 0; i < listOfResearchableParts.Count; i++)
        {
            if (partName == listOfResearchableParts[i].partName)
            {
                return listOfResearchableParts[i].buildCost[GameManager.instance.shipController.enginePartSOList.Count];
            }
        }

        return 0;
    }
    public float GetPartUpgradeTime(string partName)
    {
        for (int i = 0; i < listOfResearchableParts.Count; i++)
        {
            if (partName == listOfResearchableParts[i].partName)
            {
                return listOfResearchableParts[i].buildUpgradeTime[listOfResearchableParts[i].currentLevel];


            }
        }

        return 0;
    }
}
