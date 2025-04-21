using UnityEngine;

public class PartStats : MonoBehaviour
{
    public string partName;

    public int currentLevel;
    public int maxLevel;

    //Each Part may have more than one thing that can be upgraded....
    public int currentResearchLevel;
    public bool[] upgradeable;

    //First number in array is the build/first time cost, subsequent numbers are costs/times associated with upgrading to that particular level. This array needs to be the same size as max Level
    public int[] cost;
    public float[] buildUpgradeTime;

    public Sprite[] sprite;
    public GameObject[] gameObject;



}
