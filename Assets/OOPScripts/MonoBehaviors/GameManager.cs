using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Load and unload Options Menu Scene
    private string cancel = "Cancel";
    private string optionsScene = "Options";
    [SerializeField]
    GameObject npcPrefab;
    GameObject enemiesList;
    Transform spawnerTransform;
    private Transform playerTransform;
    public Queue<GameObject> enemyQueue;
    //
    private void Awake()
    {
        DontDestroyOnLoad(transform);
        spawnerTransform = transform;
        enemyQueue = new Queue<GameObject>();
    }
    private void Start()
    {
        enemiesList = GameObject.Find("EnemiesList");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnLoop(enemiesList,npcPrefab,2f));
    }
    void Update()
    {
        if (Input.GetButtonDown(cancel))
        {
            Scene currentScene = SceneManager.GetSceneByName(optionsScene);
            if (currentScene.name == optionsScene)
            {
                SceneManager.UnloadSceneAsync(optionsScene);
            }
            else SceneManager.LoadScene(optionsScene,LoadSceneMode.Additive); 
        }
    }
    private GameObject PoolInstantiate(GameObject enemyPrefab, Vector3 position, Quaternion rotation)
    {

        if (enemyQueue.Count != 0)
        {
            GameObject newEnemy = enemyQueue.Dequeue();
            newEnemy.transform.SetPositionAndRotation(position,rotation);
            newEnemy.transform.localScale = enemyPrefab.transform.localScale;
            newEnemy.SetActive(true);
            return newEnemy;
        }
        else
        {
            GameObject newEnemy = Instantiate(enemyPrefab, position, rotation);
            Npc newEnemyNpc = newEnemy.GetComponent<Npc>();
            newEnemyNpc.gameManager = this;
            newEnemyNpc.playerTransform = playerTransform;
            
            return newEnemy;
        }
    }
    IEnumerator SpawnLoop(GameObject enemiesList, GameObject enemy, float delay)
    {
        while (true)
        {
            if (Physics.Raycast(spawnerTransform.position, Vector3.down, out RaycastHit hitInfo, 10f))
            {
                GameObject newEnemy = PoolInstantiate(enemy, hitInfo.point, Quaternion.identity);
                newEnemy.transform.parent = enemiesList.transform;
            }                
            yield return new WaitForSeconds(delay);
        }

    }
}
