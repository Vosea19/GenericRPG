using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Stats
{
    [SerializeField]
    StatsData statsData;
    // Start is called before the first frame update
    void Start()
    {
        LoadStats(); 
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
