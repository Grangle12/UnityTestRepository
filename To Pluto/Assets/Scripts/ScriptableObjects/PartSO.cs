using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PartSO", menuName = "Scriptable Objects/PartSO")]
public class PartSO : ScriptableObject
{
    public string partName;
        
    [NonSerialized] public int currentLevel;
    public int maxLevel;

    //First number in array is the build/first time cost, subsequent numbers are costs/times associated with upgrading to that particular level. This array needs to be the same size as max Level
    public int[] cost;
    public float[] buildUpgradeTime;

    public Sprite icon;
}
