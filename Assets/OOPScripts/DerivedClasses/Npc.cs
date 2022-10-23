using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Stats
{
    [SerializeField]
    StatsData statsData;
    Transform enemyTransform;
    Transform playerTransform;
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTransform = transform;
    }
    private void OnEnable()
    {
        LoadStats();
        StartCoroutine(Move(playerTransform.position));
    }
    
    private void LoadStats()
    {
        SetMaxHealth(statsData.maxHealth);
        SetHealth(statsData.health);
        SetMaxMana(statsData.maxMana);
        SetExperience(statsData.experience);
        SetExpToLevel(statsData.expToLevel);
        SetLevel(statsData.level);
        SetMaxLevel(statsData.maxLevel);
        SetCanGainEXP(statsData.canGainExp);
        SetArmor(statsData.armor);
        SetDexterity(statsData.dexterity);
        SetStrength(statsData.strength);
        SetIntelligence(statsData.intelligence);       
    }
    override public void Die()
    {
        gameObject.SetActive(false);
    }
    IEnumerator Move(Vector3 point)
    {
        enemyTransform.LookAt(point);
        float distance = Vector3.Distance(point, transform.position);
        float speed = .4f * GetActionSpeed();
        Vector3 scaledVector = ((point - transform.position).normalized);
        int steps = Mathf.FloorToInt(distance / speed);
        for (int i = 0; i < steps; i++)
        {
            enemyTransform.position += (scaledVector * speed);
            yield return new WaitForFixedUpdate();
        }
    }
}
