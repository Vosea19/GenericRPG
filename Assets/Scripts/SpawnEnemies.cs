using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    GameObject enemiesList;
    [SerializeField]
    float delay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        enemiesList = GameObject.Find("EnemiesList");
        StartCoroutine(SpawnLoop(enemiesList,enemy,delay)); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnLoop(GameObject enemiesList,GameObject enemy, float delay)
    {
        while (true)
        {
           GameObject newEnemy = Instantiate(enemy, enemiesList.transform);
            RaycastHit[] points =  Physics.RaycastAll(transform.position, -2 * Vector3.up);
            newEnemy.transform.position = points[0].point;
            yield return new WaitForSeconds(delay);
        }
        
    }
}
