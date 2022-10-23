/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IDamageable, IInteractable
{
    private int health;
    public void Die()
    {
        gameObject.SetActive(false);
    }

    public int GetHealth()
    {
        return health;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void SetHealth(int newHealth)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        health-=damage;
        if (health <= 0)
        {
            Die();
        }
    }
}
*/