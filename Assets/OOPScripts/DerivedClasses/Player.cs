using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Stats
{
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private StatsData statsData;
    private Transform playerTransform;
    private Vector3 mousePos;
    private Coroutine moveCoroutine;
    private void Start()
    {
        playerTransform = transform;
        LoadStats();
    }
    private void Update()
    {
        mousePos = Input.mousePosition;
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            Ray pointRay = playerCamera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(pointRay, out RaycastHit point, 100f))
            {
                Vector3 position = new Vector3(point.point.x, playerTransform.position.y, point.point.z);
                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }
                moveCoroutine = StartCoroutine(Move(position));
            }
        }
    }
    override public void Die()
    {
        gameObject.SetActive(false);
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
    IEnumerator Move(Vector3 point)
    {
        playerTransform.LookAt(point);
        float distance = Vector3.Distance(point, transform.position);
        float speed = .4f * GetActionSpeed();
        Vector3 scaledVector = ((point - transform.position).normalized);
        int steps = Mathf.FloorToInt(distance / speed);
        for (int i = 0; i < steps; i++)
        {
            playerTransform.position += (scaledVector * speed);
            yield return new WaitForFixedUpdate();
        }
    }
}
