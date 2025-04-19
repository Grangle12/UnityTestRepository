using UnityEngine;

public class EnginePartStats : PartStats
{
    public Stat acceleration;
    public Stat fuelEfficiency;

    public int currentAccelerationLevel =2, currentFuelEfficiencyLevel =2 ;
    public int currentAccelerationResearchLevel =2, currentFuelEfficiencyResearchLevel = 2 ;
    public int maxAccelerationResearchLevel =5, maxFuelEfficiencyResearchLevel = 5 ;

    [SerializeField] int[] accLvlBonus = new[] { 500, 1500, 4000, 9000, 15000 };

    //Need to look at how to do this again, it was going to be float .25, .5 etc. but add modifier is int only.
    

    //Adds accLvlBonus to base engine output so at lvl 2 a 500,1500 accLvlBonus would be adding 2000 to the base value of 500 for a 2500 total
    public void LevelUpAcceleration()
    {
        currentAccelerationLevel++;

        if (currentAccelerationLevel < accLvlBonus.Length)
        {
            acceleration.AddModifier(accLvlBonus[currentAccelerationLevel]);
        }
        else
        {
            Debug.Log("There is no acceleration data for this level");
        }
    }

    /*
    public void LevelUpFuelEfficiency()
    {
        currentFuelEfficiencyLevel++;

        if (currentFuelEfficiencyLevel < fuelEfficiencyLvlBonus.Length)
        {
            Debug.Log("current FE levl = " + currentFuelEfficiencyLevel);
            Debug.Log("current FE levlbonus = " + fuelEfficiencyLvlBonus[currentFuelEfficiencyLevel]);
            Debug.Log("current FE getvalue = " + fuelEfficiency.GetValue());
            fuelEfficiency.AddModifier(fuelEfficiencyLvlBonus[currentFuelEfficiencyLevel]);
        }
        else
        {
            Debug.Log("There is no fuel efficiency data for this level");
        }
    }
    */
}
