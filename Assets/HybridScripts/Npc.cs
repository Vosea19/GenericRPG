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
    public Movement Movement;
    public Health Health;
    public Experience Experience;
    public Stats Stats;
    private void Start()
    {
        
        enemyTransform = transform;
    }
    private void OnEnable()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        LoadStats();
        StartCoroutine(Moving());
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
        Stats.SetDexterity(statsData.dexterity);
        Stats.SetStrength(statsData.strength);
        Stats.SetIntelligence(statsData.intelligence);
    }
    public void Die()
    {
        
        //gameObject.SetActive(false);
        
    }
    private void OnDisable()
    {
        gameManager.enemyQueue.Enqueue(gameObject);
    }
    IEnumerator Moving()
    {
        while (true)
        {
            Movement.StartMove(playerTransform.position, Stats.GetActionSpeed());
            yield return new WaitForSeconds(.1f);
        }
        
    }
}
