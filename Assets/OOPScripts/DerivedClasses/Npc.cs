using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Stats
{
    [SerializeField]
    StatsData statsData;
    private void OnEnable()
    {
        LoadStats();
    }
    
    private void LoadStats()
    {
        SetMaxHealth(statsData.maxHealth);
        SetHealth(statsData.health);
        SetMaxMana(statsData.maxMana);
        SetExperience(statsData.experience);
        SetExpToLevel(statsData.expToLevel);
        SetLevel(statsData.level);
        SetMaxLevel(statsData.maxLevel);
        SetCanGainEXP(statsData.canGainExp);
        SetArmor(statsData.armor);
        SetDexterity(statsData.dexterity);
        SetStrength(statsData.strength);
        SetIntelligence(statsData.intelligence);       
    }
    override public void Die()
    {
        gameObject.SetActive(false);
    }
}
