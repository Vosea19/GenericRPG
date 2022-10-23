using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces : MonoBehaviour
{
 
}
interface IDamageable
{
    int GetHealth();
    void SetHealth(int newHealth);
    void Die();

    void TakeDamage(int damage);
}
interface ILootable
{
    GameObject Pickup();
}

interface IInteractable
{
    void Interact();
}


