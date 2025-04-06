using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void SaveGame()
    {
        GameManager.instance.UpdateCoinCount();
        Debug.Log("SCene saving: " + SceneManagerScript.instance.sceneName);
        SaveObject saveObject = new SaveObject
        {
            // This or saveManager to get a scene from the save?
            sceneName = SceneManagerScript.instance.sceneName.ToString(),
            saveTime = GameManager.instance.currentTime,
            saveCoinAmount = GameManager.instance.coinAmount,
            carPosition = GameManager.instance.carGameObject.transform.position,
            carMovement = GameManager.instance.playerUnit,
            saveCoinDictionary = GameManager.instance.coinDictionary

        };
        string json = JsonUtility.ToJson(saveObject);

        SaveSystem.Save(json);
    }

    public void LoadGame()
    {

        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            Debug.Log("Loaded: " + saveString);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            // This or saveManager to get a scene from the save?
            SceneManagerScript.instance.LoadGameFromOtherScene(saveObject);
        }
    }
}
