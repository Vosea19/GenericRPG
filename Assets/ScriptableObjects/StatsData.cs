using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPCData")]
public class StatsData : ScriptableObject

{
    public int maxHealth = 100;
    public int health = 100;
    public int maxMana = 100;
    public int mana = 100;
    public int experience = 0;
    public int expToLevel = 1000;
    public int level = 1;
    public int maxLevel = 100;
    public bool canGainExp = true;
    public float expIncreasePerLevel = 1.2f;
    public int armor = 0;
    public int dexterity = 0;
    public int strength = 0;
    public int intelligence = 0;
}
