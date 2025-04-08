using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

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
    public void SaveGame(bool newSave)
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
        

        //THE BELOW IS TO BE USED TO POPULATE A LIST OF BUTTONS FOR THE SAVE PANEL WITH SAVED GAMES ON THEM.
       
        
        //MenuManager.instance.PopulateSaveMenu();
        //string saveName = MenuManager.instance.ChooseSave();
        if (!newSave)
        {
            Debug.Log("the name is: " + EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text);
            SaveSystem.Save(json, EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text);
            MenuManager.instance.CloseAllMenus();
        }
        else
        {
            SaveSystem.Save(json);
            MenuManager.instance.CloseAllMenus();
        }
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
