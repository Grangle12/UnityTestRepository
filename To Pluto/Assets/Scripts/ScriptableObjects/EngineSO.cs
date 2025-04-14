using UnityEngine;

[CreateAssetMenu(fileName = "EngineSO", menuName = "Scriptable Objects/EngineSO")]
public class EngineSO : ScriptableObject
{
    public string engineName;

    public int currentLevel;

    public float[] fuelEfficiency;
    public int[] acceleration;

    public int[] cost;
    public float[] buildTime;

    public Sprite[] sprite;
    public GameObject[] gameObject;
}
