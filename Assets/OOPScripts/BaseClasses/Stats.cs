using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats : MonoBehaviour
{
    private int maxHealth;
    private int health;
    private int maxMana;
    private int mana;
    private int experience;
    private int expToLevel = 1000;
    private int level;
    private int maxLevel;
    private bool canGainExp = true;
    private float expIncreasePerLevel = 1.2f;
    private int armor;
    private int dexterity;
    private int strength;
    private int intelligence;
    private float actionSpeed;
    private int spellDamage;
    private int attackDamage;
     
    #region Defences
    public void SetHealth(int newHealth)
    {
        health = health <= maxHealth ? health : maxHealth;
        if (health <= 0)
        {
            Die();
        }

    }
    public int GetHealth()
    {
        return health;
    }
    public void SetMaxHealth(int newMaxhealth)
    {
        maxHealth = newMaxhealth > 0? newMaxhealth : maxHealth ;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public void SetArmor(int newArmor)
    {
        armor = newArmor >= 0 ? newArmor : armor;
    }
    public int GetArmor()
    {
        return armor;
    }
    public void TakeDamage(int baseDamage)
    {
        if (baseDamage < 0)
        {
            throw new ArgumentException("Cannot take negative damage");
        }
        int totalDamage = Mathf.FloorToInt((float)baseDamage * (100f / (100f + (float)armor)));
        SetHealth(health - totalDamage);
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
    #endregion
    #region Experience
    public void SetExperience(int newExperience)
    {
        if (!canGainExp)
        {
            return;
        }
        experience = newExperience >= 0 && newExperience >= experience ? newExperience : experience;
        if (experience >= expToLevel)
        {
            LevelUp();
        }
    }
    public int GetExperience()
    {
        return experience;
    }
    public void LevelUp()
    {
        level = level > 0 && level < maxLevel ? level + 1 : level;
        expToLevel = Mathf.FloorToInt((float)expToLevel * expIncreasePerLevel);
        if (level == maxLevel)
        {
            canGainExp = false;
        }
    }
    #endregion
    #region Stats
    public void SetMana(int newMana)
    {
        if (newMana <= 0)
        {
            mana = 0;
        }
    }
    public int GetMana()
    {
        return mana;
    }
    public void SetMaxMana(int newMaxMana)
    {
        if (newMaxMana >= 0)
        {
            maxMana = newMaxMana;
        }
        else
        {
            throw new ArgumentException("Max Mana Cannot be set to a negative value");
        }
        
    }
    public int GetMaxMana()
    {
        return maxMana;
    }
    public bool SpendMana(int manaToSpend)
    {
        if (manaToSpend > mana)
        {
            return false;
        }
        else
        {
            SetMana(mana - manaToSpend);
            return true;

        }

    }
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
