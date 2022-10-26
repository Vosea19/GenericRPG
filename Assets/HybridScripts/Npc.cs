using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Npc : MonoBehaviour
{
    [SerializeField]
    StatsData statsData;
    Transform enemyTransform;
    public Transform playerTransform;
    public GameManager gameManager;
    public GameObject nameplate;
    public Movement Movement;
    public Health Health;
    public Experience Experience;
    public Stats Stats;
    private void Start()
    {
        playerTransform = gameManager.playerTransform;
        enemyTransform = transform;
    }
    private void OnEnable()
    {
        LoadStats();
        if (playerTransform != null)
        {
            Movement.StartMove(playerTransform.position, Stats.GetActionSpeed()) ;
        }    
    }
    private void LoadStats()
    {
        Health.SetMaxHealth(statsData.maxHealth);
        Health.SetHealth(statsData.health);
        Health.SetArmor(statsData.armor);
        Experience.SetExpToLevel(statsData.expToLevel);
        Experience.SetLevel(statsData.level);
        Experience.SetMaxLevel(statsData.maxLevel);
        Experience.SetExperience(statsData.experience);
        Experience.SetCanGainEXP(statsData.canGainExp);      
    }
    public void Die()
    {
        gameManager.enemyQueue.Enqueue(gameObject);
        gameObject.SetActive(false);
        
    }
}
