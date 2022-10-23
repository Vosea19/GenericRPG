using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventory;
    public int coinCount;
    private string inventoryScene = "Inventory";
    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Scene currentScene = SceneManager.GetSceneByName(inventoryScene);
            if (currentScene.name == inventoryScene)
            {
                GameObject root = GameObject.Find("SlotRoot");
                root.GetComponent<InventoryVisualizer>().HideItems();
                SceneManager.UnloadSceneAsync(inventoryScene);
            }
            else SceneManager.LoadScene(inventoryScene, LoadSceneMode.Additive);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Loot"))
        {
            if (inventory.Count < 32)
            {
                inventory.Add(other.gameObject);
                other.gameObject.SetActive(false);
            }
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
    }
}
