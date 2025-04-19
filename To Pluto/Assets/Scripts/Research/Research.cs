using UnityEngine;

public class Research : MonoBehaviour
{
    [SerializeField] enum Stat { EngineFuelEfficiency, EngineAcceleration, Detector }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float timer;

    bool localRsrchUpdating;

    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void ResearchLevel(int resrchNum)
    {
        

            Debug.Log("Researching Engine level");
            //if (!updating)
            //  StartCoroutine(ResearchCoroutine("engineLevel", resrchEngineLevel));
            //updateButton1 = true;
            //researchPanel.SetActive(false);
            //UnPopulateResearch();
        
    }
}
