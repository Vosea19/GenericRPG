using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private int dexterity;
    private int strength;
    private int intelligence;
    private float actionSpeed;
    private int spellDamage;
    private int attackDamage;  
    #region Stats
   
    public void SetDexterity(int newDexterity)
    {
        dexterity = newDexterity >= 0 ? newDexterity : dexterity;
        actionSpeed = 1f * (1 + dexterity / 10f);
    }
    public int GetDexterity()
    {
        return dexterity;
    }
    public void SetStrength(int newStrength)
    {
        strength = newStrength >= 0 ? newStrength : strength;
        attackDamage = 10 * (1 + strength / 10);
    }
    public int GetStrength()
    {
        return strength;
    }
    public void SetIntelligence(int newIntelligence)
    {
        intelligence = newIntelligence >= 0 ? newIntelligence : intelligence;
        spellDamage = 10 * (1 + intelligence / 10);
    }
    public int GetIntelligence()
    {
        return intelligence;
    }
    #endregion
    #region Offencives
    public float GetActionSpeed()
    {
        return actionSpeed;
    }
    public int GetAttackDamage()
    {
        return attackDamage;
    }
    public int GetSpellDamage()
    {
        return spellDamage;
    }
    #endregion
  
}
