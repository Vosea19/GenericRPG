using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    private int experience;
    private int expToLevel;
    private int level;
    private int maxLevel = 100;
    private bool canGainExp;
    private float expIncreasePerLevel;

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
    public void SetLevel(int newLevel)
    {
        if (newLevel > maxLevel || newLevel <= 0)
        {
            throw new ArgumentException("Level cannot exceed MaxLevel or be less than 1");
        }
        level = newLevel;
    }
    public int GetMaxLevel()
    {
        return maxLevel;
    }
    public void SetMaxLevel(int newMaxLevel)
    {
        if (newMaxLevel < level || newMaxLevel <= 0)
        {
            throw new ArgumentException("MaxLevel cannot be less than currentLevel or less than 1");
        }
        maxLevel = newMaxLevel;
    }
    public int GetLevel()
    {
        return level;
    }
    public void SetExpToLevel(int newExpToLevel)
    {
        if (newExpToLevel <= 0)
        {
            throw new ArgumentException("ExpToLevel cannot be less than 1");
        }
        expToLevel = newExpToLevel;
    }
    public void SetCanGainEXP(bool newCanGainExp)
    {
        canGainExp = newCanGainExp;
    }
    public bool GetCanGainEXP()
    {
        return canGainExp;
    }
}
