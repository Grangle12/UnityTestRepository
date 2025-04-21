using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{

    [SerializeField]
    private int baseValue;

    [SerializeField] int[] levelValue; // = new int[] ;

    [SerializeField]
    private List<int> modifiers = new List<int>();

    [SerializeField]
    private int currentLevel;
    
    [SerializeField]
    private int maxLevel;

    public Sprite icon;
    public Color iconColor;

    public int GetCurrentLevel()
    {
        return currentLevel;
    }



    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(X => finalValue += X);
        return finalValue;
    }

    public bool IsMaxLevel()
    {
        if (currentLevel == maxLevel)
            return true;
        else
            return false;
    }
    public void LevelUp()
    {
        if (currentLevel < maxLevel)
        {
            if (currentLevel < levelValue.Length)
            {
                baseValue = levelValue[currentLevel];
                currentLevel++;
            }
            else
            {
                Debug.LogWarning("trying to add level with no existing value");
            }
        }
        else
        {
            Debug.Log("current Level is equal to or over Max level for this stat");
        }
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            Debug.Log("we got to here");
            modifiers.Add(modifier);
            Debug.Log("we also got to here");
        }

    }
    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }

    }
}
