using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public int damage;
    public int castTime;
    public float distance;
    public float speed;
    public int manaCost;
    public float chainDistance;
    public int numChains;
    public bool multipleProjectiles;
    public int numOfProjectiles;
    public ProjectileMechanic projectileMechanic;
}
public enum ProjectileMechanic{None = 0,Pierce = 1,Chain = 2,Channel = 3};
