using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject
{ 
    
    public string sceneName;

    public float saveTime;

    public int saveCoinAmount;
    public Vector3 carPosition;
    public CarMovement carMovement;
    public SerializableDictionary<int, bool> saveCoinDictionary;

}
