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
                a = Random.Range(0, TempList1.Count - 1);
                for (int y = 0; y < listOfResearchableParts.Count; y++)
                {
                    

                    if (listOfResearchableParts[y].partName == TempList1[a].partName)
                    {
                        //Debug.Log("were saying " + listOfResearchableParts[y].partName + " is equal to " + TempList1[a].partName + " and i, y =" + i+ ", " + y);

                        buttonList[i].onClick.AddListener(delegate { AssignFillImage(EventSystem.current.currentSelectedGameObject); });
                        buttonList[i].onClick.AddListener(() => UpgradePartSO(listOfResearchableParts[y]));
                        buttonList[i].transform.gameObject.GetComponent<Image>().sprite = listOfResearchableParts[y].icon;
                        

                        Debug.Log(buttonList[i].gameObject + " is supposed to be the fill image ");

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
        if (!researching)
        {
            Debug.Log(part);
            if (GameManager.instance.shipController.resourceCount >= part.cost[part.currentLevel])
            {
                Debug.Log("researching");
                GameManager.instance.shipController.resourceCount -= part.cost[part.currentLevel];
                researching = true;
                StartCoroutine(ResearchCoroutine(part));
            }
        }
    }
    IEnumerator ResearchCoroutine(PartSO part)
    {
        resrchtime = part.buildUpgradeTime[part.currentLevel];
        Debug.Log("please wait..." + part.buildUpgradeTime[part.currentLevel]);
        yield return new WaitForSeconds(part.buildUpgradeTime[part.currentLevel]);

        part.currentLevel++;
        timer = 0;
        UpdateFillAmount(displayManager.upgradeButtonFillImage, timer / 0);
        researching = false;
        Debug.Log("Research of " + part + " is complete.");
    }

}
