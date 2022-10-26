using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    //Load and unload Options Menu Scene
    private string cancel = "Cancel";
    private string optionsScene = "Options";
    [SerializeField]
    GameObject npcPrefab;
    GameObject poolList;
    GameObject enemiesList;
    Transform spawnerTransform;
    public Transform playerTransform;
    public Queue<GameObject> enemyQueue;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public static GameManager Instance;
    //
    private void Awake()
    {
        DontDestroyOnLoad(transform);
        Instance = this;
        spawnerTransform = transform;
        enemyQueue = new Queue<GameObject>();
        poolList = GameObject.Find("PoolList");
        enemiesList = GameObject.Find("EnemiesList");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab,poolList.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
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
    IEnumerator SpawnLoop(GameObject enemiesList, GameObject enemy, float delay)
    {
        while (true)
        {
            if (Physics.Raycast(spawnerTransform.position, Vector3.down, out RaycastHit hitInfo, 10f))
            {
                GameObject newEnemy = PoolInstantiate("Enemy", hitInfo.point, Quaternion.identity);
                //newEnemy.transform.parent = enemiesList.transform;
            }                
            yield return new WaitForSeconds(delay);
        }

    }
    public GameObject PoolInstantiate(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag" + tag + "Doesnt Exist");
            return null;
        }
        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.SetPositionAndRotation(position, rotation);
        poolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }
    
}
