using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ResearchManager : MonoBehaviour
{
    [SerializeField] List<Button> buttonList = new List<Button>();
    [SerializeField] GameObject researchPanel;

    //RESEARCH
    int resrchEngineLevel = 1;
    int resrchEngineEfficiency = 1;
    int resrchDetectorLevel = 1;
    int resrchProbeLevel = 1;
    int resrchMineLevel = 1;
    int resrchSpeedLevel = 1;

    List<int> resrchList = new List<int>();

    float resrchSpeed = 1.5f;
    float resrchtime = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //This chooses 3 random researches to research
    public void PopulateResearch()
    {

        researchPanel.SetActive(true);
        buttonList[0].onClick.AddListener(ResearchEngineLevel);
        buttonList[1].onClick.AddListener(ResearchEngineEfficiency);
        buttonList[2].onClick.AddListener(ResearchProbeLevel);

        
    }

    public void UnPopulateResearch()
    {
        for(int i =0; i < buttonList.Count; i++)
        {
            buttonList[i].onClick.RemoveAllListeners();
        }
    }

    public void ResearchEngineLevel()
    {
        Debug.Log("Researching Engine level");
        StartCoroutine(ResearchCoroutine("engineLevel", resrchEngineLevel));
        researchPanel.SetActive(false);
        UnPopulateResearch();
    }

    public void ResearchEngineEfficiency()
    {
        Debug.Log("Researching Engine Efficiency level");
        StartCoroutine(ResearchCoroutine("engineEfficiency", resrchEngineEfficiency));
        researchPanel.SetActive(false);
        UnPopulateResearch();
    }

    public void ResearchDetectorLevel()
    {
        Debug.Log("Researching Detector level");
        StartCoroutine(ResearchCoroutine("detector", resrchDetectorLevel));
        researchPanel.SetActive(false);
        UnPopulateResearch();
    }
    public void ResearchProbeLevel()
    {
        Debug.Log("Researching Probe level");
        StartCoroutine(ResearchCoroutine("probe",resrchProbeLevel));
        researchPanel.SetActive(false);
        UnPopulateResearch();
    }

    IEnumerator ResearchCoroutine(string researchType, int research)
    {
        
        yield return new WaitForSeconds(resrchtime);
        
        if(researchType == "engineLevel")
        {
            resrchEngineLevel++;
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

}
