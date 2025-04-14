using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CheckPointSO", menuName = "Scriptable Objects/CheckPointSO")]
public class CheckPointSO : ScriptableObject
{
    public string checkPointName;

    public double distanceFromSun;

    public float slingShotBonus;

    public bool hasArrived = false;

    public Sprite sprite;
}
