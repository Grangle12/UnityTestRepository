using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Level_1", LoadSceneMode.Single);
        
    }
    public void LoadScene(string s)
    {
        SceneManager.LoadScene(s, LoadSceneMode.Single);
        PauseManager.instance.UnpauseGame();
    }
}
