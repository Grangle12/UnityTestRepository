using UnityEngine;

[CreateAssetMenu(fileName = "Resource_SO", menuName = "Scriptable Objects/Resource_SO")]
public class Resource_SO : ScriptableObject
{
    public int level;
    
    public float fuelAmt;
    public float fuelAmtVariance;
    
    public float rareResourceAmt;
    public float rareResourceAmtVariance;

    public Sprite sprite;
    public GameObject gameObject;

}
