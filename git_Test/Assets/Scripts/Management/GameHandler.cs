using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    [SerializeField] private GameObject carGameObject;
    int goldAmountReal = 0;
    private CarMovement carMovement;


    private void Awake()
    {
        SaveSystem.Init();

        carMovement = carGameObject.GetComponent<CarMovement>();

        SaveObject saveObject = new SaveObject
        {
            goldAmount = 5,
            carPosition = carGameObject.transform.position
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
            Debug.Log("p pressed - Saving Game....");
            Save();
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            goldAmountReal++;
            Debug.Log("current gold is: " + goldAmountReal);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("l pressed");
            Load();
            Debug.Log("current gold is: " + goldAmountReal);
        }
    }

    private void Save()
    {
        SaveObject saveObject = new SaveObject
        {
            goldAmount = goldAmountReal,
            carPosition = carGameObject.transform.position
        };
        string json = JsonUtility.ToJson(saveObject);

        SaveSystem.Save(json);
    }

    private void Load()
    {
        string saveString = SaveSystem.Load();
        if(saveString != null)
        {
            Debug.Log("Loaded: " + saveString);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            goldAmountReal = saveObject.goldAmount;
            carGameObject.transform.position = saveObject.carPosition;
        }
    }

    private class SaveObject
    {
        public int goldAmount;
        public Vector3 carPosition;
        public CarMovement carMovement;
    }

}
