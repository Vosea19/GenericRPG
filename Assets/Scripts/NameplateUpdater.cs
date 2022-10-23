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

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > lastEnemyCheck + 0.1f)
        {
            FindEnemies();
        }
        //int enemyCount = enemies.Count;
        foreach (var enemy in enemies)
        {
            Vector3 viewPos = playerCamera.WorldToViewportPoint(enemy.transform.position);
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                if (enemy.TryGetComponent<Npc>(out Npc enemyHealth) && enemyHealth.nameplate != null)
                {
                    GameObject currNameplate = enemyHealth.nameplate;
                    enemyHealth.nameplate = currNameplate;
                    RectTransform nameplateReal = currNameplate.GetComponent<RectTransform>();
                    nameplateReal.position = playerCamera.WorldToScreenPoint(enemy.transform.position + Vector3.up * 2);
                    currNameplate.transform.position = nameplateReal.position;
                }
                ;
            }
            else
            {
                GameObject enemyNameplate = enemy.GetComponent<Npc>().nameplate;
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
                child.GetComponent<Npc>().OnHealthChange += UpdateNameplate;                
            }
            if (child.gameObject.activeInHierarchy == false && enemies.Contains(child.gameObject) == true)
            {
                enemies.Remove(child.gameObject);
                child.GetComponent<Npc>().OnHealthChange -= UpdateNameplate;

            }
        }
        lastEnemyCheck = timer;
    }
    GameObject CheckNameplates()
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
    private void UpdateNameplate(object sender, Npc.OnHealthChangeEventArgs e)
    {
        Vector3 viewPos = playerCamera.WorldToViewportPoint(e.passedObject.transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            if (e.passedObject.TryGetComponent<Npc>(out Npc enemyHealth))
            {
                GameObject currNameplate = enemyHealth.nameplate;
                if (e.passedObject.activeInHierarchy)
                {
                    if (enemyHealth.nameplate == null)
                    {
                        currNameplate = CheckNameplates();
                    }
                    enemyHealth.nameplate = currNameplate;
                    //RectTransform nameplateReal = currNameplate.GetComponent<RectTransform>();
                    //nameplateReal.position = playerCamera.WorldToScreenPoint(e.passedObject.transform.position + Vector3.up * 2);
                    //enemyHealth.SetNameplatePosition(nameplateReal.position);
                    currNameplate.transform.GetChild(0).GetComponent<Image>().fillAmount = (float)enemyHealth.GetHealth() / (float)enemyHealth.GetMaxHealth();
                    currNameplate.transform.GetChild(1).GetComponent<Text>().text = enemyHealth.GetHealth().ToString() + "/" + enemyHealth.GetMaxHealth().ToString();
                    currNameplate.SetActive(true);
                }
            }
            ;
        }
    }
    
}

