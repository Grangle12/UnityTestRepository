using UnityEngine;

[CreateAssetMenu(fileName = "EngineSO", menuName = "Scriptable Objects/EngineSO")]
public class EngineSO : ScriptableObject
{
    public string engineName;

    public int currentLevel;
    public int currentResearchLevelFE;
    public int currentResearchLevelAcc;
    public int maxLevel;

    public float[] fuelEfficiency;
    public int[] acceleration;

    public int[] cost;
    public float[] buildUpgradeTime;

    public Sprite[] sprite;
    public GameObject[] gameObject;
}
