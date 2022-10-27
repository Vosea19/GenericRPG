using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private int maxHealth = 100;
    private int health = 100;
    private int armor;
    public GameObject nameplate;

    public event EventHandler<OnHealthChangeEventArgs> OnHealthChange;
    public class OnHealthChangeEventArgs : EventArgs
    {
        public GameObject passedObject;
    }
    public void SetHealth(int newHealth)
    {
        health = newHealth <= maxHealth ? newHealth : maxHealth;
        OnHealthChange?.Invoke(this, new OnHealthChangeEventArgs { passedObject = this.gameObject });
        UpdateNameplate();
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
        maxHealth = newMaxhealth > 0 ? newMaxhealth : maxHealth;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
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
    public void SetArmor(int newArmor)
    {
        armor = newArmor >= 0 ? newArmor : armor;
    }
    public int GetArmor()
    {
        return armor;
    }
    public void Die()
    {
        if (nameplate != null)
        {
            print("Got to 1");
            nameplate.SetActive(false);
            //nameplate = null;
        }
        print("Got to 2");
        gameObject.SetActive(false);
    }
    public void UpdateNameplate()
    {
        if (nameplate!=null)
        {
            if (!nameplate.activeInHierarchy)
            {
                nameplate.SetActive(true);
            }
            nameplate.transform.GetChild(0).GetComponent<Image>().fillAmount = (float)GetHealth() / (float)GetMaxHealth();
            nameplate.transform.GetChild(1).GetComponent<Text>().text = GetHealth().ToString() + "/" + GetMaxHealth().ToString();
        }
    }
}
