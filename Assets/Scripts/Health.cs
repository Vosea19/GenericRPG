using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Health : MonoBehaviour, IDamageable
{
    private int maxHealth = 100;
    private int currentHealth = 100;
    private int healthRegen;
    public GameObject damageText;
    private GameObject ui;
    private Vector3 nameplatePosition = new Vector3(0, 0, 0);
    public GameObject nameplate;
    private WaitForSeconds regenTime = new WaitForSeconds(1f);

    public event EventHandler<OnHealthChangeEventArgs> OnHealthChange;
    public class OnHealthChangeEventArgs : EventArgs
    {
        public GameObject passedObject;
    }

    public void SetHealth(int newHealth)
    {
        currentHealth = newHealth;
        if (newHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
        OnHealthChange?.Invoke(this, new OnHealthChangeEventArgs { passedObject = this.gameObject });
    }
    public int GetHealth()
    {
        return currentHealth;
    }
    public int GetMaxHealth() { 
        return maxHealth;
    }
    void Start()
    {
        ui = GameObject.Find("PlayerUI");
        currentHealth = maxHealth;
        healthRegen = 1;
        StartCoroutine(HealthRegen());
    }

    public void TakeDamage(int damage)
    {

        //FloatingText(damage);
        SetHealth(currentHealth - damage);
        
    }
   
    IEnumerator HealthRegen()
    {
        while (true)
        {
            SetHealth(currentHealth += healthRegen);
            yield return regenTime;
        }
    }
    public void SetNameplatePosition(Vector3 position)
    {
        nameplatePosition = position;
    }
    public void Die()
    {
        
        
        if(CompareTag("Player")) return;
        if (transform.TryGetComponent<Loot>(out Loot loot))
        {
           Inventory playerInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
           playerInventory.coinCount += loot.GetCoins();
        }
        nameplate.SetActive(false);
        gameObject.SetActive(false);
    }
    private void FloatingText(int damage)
    {
        GameObject floatingText = Instantiate(damageText, ui.transform);
        floatingText.transform.position = nameplatePosition + Vector3.up * 20 + Vector3.right * UnityEngine.Random.Range(-50, 51);
        Text text = floatingText.GetComponent<Text>();
        if (damage > 0)
        {
            text.text = "-" + damage.ToString();
            text.color = Color.red;
        }
        else
        {
            text.text = "+" + damage.ToString();
            text.color = Color.green;
        }
    }
}
