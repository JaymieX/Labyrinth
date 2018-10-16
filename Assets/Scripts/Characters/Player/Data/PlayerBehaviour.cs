using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour
{
    // Player name
    public string Name { get; set; }

    // Base stats
    public float BaseWalkSpeed { get; set; } // Walking speed
    public float BaseHealth { get; set; } // Max health
    public float BaseLuck { get; set; } // Luck percentage
    public float BaseGearRate { get; set; } // Base money rate from loot

    public ICommand BaseMeleeDamage { get; set; } // Base melee damage
    public ICommand BaseRangeDamage { get; set; } // Base range damage

    public PlayerBehaviour(string name, float bWalk, float bHp, float bLuck, float bGear, ICommand cMelee, ICommand cRange)
    {
        Name = name;

        BaseWalkSpeed = bWalk;
        BaseHealth = bHp;
        BaseLuck = bLuck;
        BaseGearRate = bGear;
        BaseMeleeDamage = cMelee;
        BaseRangeDamage = cRange;
    }

    public void ExecuteMelee()
    {
        BaseMeleeDamage.Execute();
    }

    public void ExecuteRange()
    {
        BaseRangeDamage.Execute();
    }
}
