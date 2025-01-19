using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    int goldAmountReal = 0;

    private void Awake()
    {
        SaveObject saveObject = new SaveObject
        {
            goldAmount = 5
        };
        string json = JsonUtility.ToJson(saveObject);
        Debug.Log("saved = " + json);

        SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(json);
        Debug.Log("loaded = " + loadedSaveObject.goldAmount);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("p pressed");
            Save();
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            goldAmountReal++;
            Debug.Log("current gold is: " + goldAmountReal);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("p pressed");
            Load();
        }
    }

    private void Save()
    {
        SaveObject saveObject = new SaveObject
        {
            goldAmount = goldAmountReal
        };
        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    private void Load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");

            Debug.Log("Loaded: " + saveString);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            goldAmountReal = saveObject.goldAmount;
        }
    }

    private class SaveObject
    {
        public int goldAmount;
    }

}
