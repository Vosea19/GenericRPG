using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mana))]
[RequireComponent(typeof(Experience))]
[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(Movement))]
//[RequireComponent(typeof(Health))]
//[RequireComponent(typeof(Health))]

public class Player : MonoBehaviour
{
    private Camera playerCamera;
    [SerializeField]
    private StatsData statsData;
    private Transform playerTransform;
    private Vector3 mousePos;
    #region Class Components
    public Movement Movement;
    public Health Health;
    public Mana Mana;
    public Experience Experience;
    public Stats Stats;
    #endregion
    private void Awake()
    {
        playerTransform = transform;
        playerCamera = Camera.main;
        Movement = GetComponent<Movement>();
        Health = GetComponent<Health>();
        Mana = GetComponent<Mana>();
        Experience = GetComponent<Experience>();
        Stats = GetComponent<Stats>();
        LoadStats();
    }
    private void Update()
    {
        mousePos = Input.mousePosition;
        Ray pointRay = playerCamera.ScreenPointToRay(mousePos);
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(pointRay, out RaycastHit point, 100f))
            {
                Movement.StartMove(point.point,Stats.GetActionSpeed());
            }
        }
    }
    private void LoadStats()
    {
        Health.SetMaxHealth(statsData.maxHealth);
        Health.SetHealth(statsData.health);
        Health.SetArmor(statsData.armor);
        Mana.SetMaxMana(statsData.maxMana);
        Mana.SetMana(statsData.mana);
        Experience.SetExperience(statsData.experience);
        Experience.SetExpToLevel(statsData.expToLevel);
        Experience.SetLevel(statsData.level);
        Experience.SetMaxLevel(statsData.maxLevel);
        Experience.SetCanGainEXP(statsData.canGainExp);
        Stats.SetDexterity(statsData.dexterity);
        Stats.SetStrength(statsData.strength);
        Stats.SetIntelligence(statsData.intelligence);
    }
    
}
