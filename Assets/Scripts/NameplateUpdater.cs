using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NameplateUpdater : MonoBehaviour
{
    private Camera playerCamera;
    public GameObject enemyNameplate;
    private List<GameObject> enemies;
    private List<GameObject> nameplates;
    public Transform enemyParent;
    public GameObject UI;
    private float timer = 0;
    private float lastEnemyCheck = 0;
    float count = 100;
    // Start is called before the first frame update
    void Start()
    {
        
        enemies = new List<GameObject>();
        nameplates = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject nameplate = Instantiate(enemyNameplate, UI.transform);
            nameplate.SetActive(false);
            nameplates.Add(nameplate);
        }

        playerCamera = Camera.main;

    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > lastEnemyCheck + 0.1f)
        {
            FindEnemies();
        }
        foreach (var enemy in enemies)
        {
            Vector3 viewPos = playerCamera.WorldToViewportPoint(enemy.transform.position);
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                if (enemy.TryGetComponent<Npc>(out Npc enemyNpc) && enemy.activeInHierarchy)
                {
                    if (enemyNpc.Health.nameplate == null)
                    {
                        enemyNpc.Health.nameplate = CheckNameplates();

                    }
                    RectTransform nameplateRec = enemyNpc.Health.nameplate.GetComponent<RectTransform>();
                    nameplateRec.position = playerCamera.WorldToScreenPoint(enemy.transform.position + Vector3.up * 2);
                    //enemyNpc.Health.nameplate.SetActive(true);
                }
                ;
            }
            else
            {
                GameObject enemyNameplate = enemy.GetComponent<Npc>().Health.nameplate;
                if (enemyNameplate != null)
                {
                    enemyNameplate.SetActive(false);
                }
                
            }
        }
    }
    public void FindEnemies()
    {
        foreach (Transform child in enemyParent)
        {
            if (child.gameObject.activeInHierarchy && enemies.Contains(child.gameObject) == false)
            {
                enemies.Add(child.gameObject);
                //child.GetComponent<Health>().OnHealthChange += UpdateNameplate;                
            }
            if (child.gameObject.activeInHierarchy == false && enemies.Contains(child.gameObject) == true)
            {
                enemies.Remove(child.gameObject);
                //child.GetComponent<Health>().OnHealthChange -= UpdateNameplate;

            }
        }
        lastEnemyCheck = timer;
    }
   public GameObject CheckNameplates()
    {
        foreach (var nameplate in nameplates)
        {
            if (!nameplate.activeInHierarchy)
            {
                return nameplate;
            }

        }
        return null;
    }
    /*
    private void UpdateNameplate(object sender, Health.OnHealthChangeEventArgs e)
    {
        Vector3 viewPos = playerCamera.WorldToViewportPoint(e.passedObject.transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            if (e.passedObject.TryGetComponent<Health>(out Health enemyHealth))
            {
                GameObject currNameplate = e.passedObject.GetComponent<Npc>().nameplate;
                if (e.passedObject.activeInHierarchy)
                {
                    if (currNameplate == null)
                    {
                        currNameplate = CheckNameplates();
                    }
                    currNameplate.transform.GetChild(0).GetComponent<Image>().fillAmount = (float)enemyHealth.GetHealth() / (float)enemyHealth.GetMaxHealth();
                    currNameplate.transform.GetChild(1).GetComponent<Text>().text = enemyHealth.GetHealth().ToString() + "/" + enemyHealth.GetMaxHealth().ToString();
                    currNameplate.SetActive(true);
                }
            }
            ;
        }
    }
    */
}

