using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;
    public string sceneName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        sceneName = SceneManager.GetActiveScene().name;
    }
    public void LoadGameFromOtherScene(SaveObject saveObject)
    {
        //SceneManager.LoadScene(s, LoadSceneMode.Single);
        if(SceneManager.GetActiveScene().name != saveObject.sceneName)
        { 
            var sceneVar = SceneManager.LoadSceneAsync(saveObject.sceneName, LoadSceneMode.Single);
            sceneVar.completed += (x) =>
             {
                 //GameManager.instance.Load();
                 if (PauseManager.instance != null)
                 {
                     PauseManager.instance.UnpauseGame();
                 }
                 GameManager.instance.currentTime = saveObject.saveTime;
                 GameManager.instance.coinAmount = saveObject.saveCoinAmount;
                 GameManager.instance.GetComponent<DisplayText>().UpdateCoinCounter();

                 GameManager.instance.carGameObject.transform.position = saveObject.carPosition;
                 GameManager.instance.playerUnit = saveObject.carMovement;
                 GameManager.instance.coinDictionary = saveObject.saveCoinDictionary;

                 foreach (var coin in GameManager.instance.coins)
                 {
                     coin.LoadCoinData();
                 }
             };

        }
        else
        {
            if (PauseManager.instance != null)
            {
                PauseManager.instance.UnpauseGame();
            }
            GameManager.instance.currentTime = saveObject.saveTime;
            GameManager.instance.coinAmount = saveObject.saveCoinAmount;
            GameManager.instance.GetComponent<DisplayText>().UpdateCoinCounter();

            GameManager.instance.carGameObject.transform.position = saveObject.carPosition;
            GameManager.instance.playerUnit = saveObject.carMovement;
            GameManager.instance.coinDictionary = saveObject.saveCoinDictionary;

            foreach (var coin in GameManager.instance.coins)
            {
                coin.LoadCoinData();
            }
        }

    }
    public void LoadScene(string s)
    {
        SceneManager.LoadScene(s, LoadSceneMode.Single);
        if (PauseManager.instance != null)
        {
            PauseManager.instance.UnpauseGame();
        }
    }

 
}
