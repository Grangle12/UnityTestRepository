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
        

        if (!newSave)
        {
           
            SaveSystem.Save(json, EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text);
            MenuManager.instance.CloseAllMenus();
        }
        else
        {
            SaveSystem.Save(json);
            MenuManager.instance.CloseAllMenus();
        }
    }

    //Loads file into a saveObject class
    public void LoadGame(bool loadMostRecent)
    {
        if (loadMostRecent)
        {
            string saveString = SaveSystem.Load();
            if (saveString != null)
            {
                Debug.Log("Loaded: " + saveString);

                SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
                
                //Save Object is then opened to actually load information into scene
                SceneManagerScript.instance.LoadGameFromOtherScene(saveObject);
            }
        }
        else
        {
            string saveString = SaveSystem.Load(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text);
            if (saveString != null)
            {
                
                Debug.Log("Loaded: " + saveString);

                SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
                
                //Save Object is then opened to actually load information into scene
                SceneManagerScript.instance.LoadGameFromOtherScene(saveObject);
            }
        }
        MenuManager.instance.CloseAllMenus();
    }
}
