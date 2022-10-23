using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private List<GameObject> loot;
    [SerializeField]
    GameObject coinObject;
    int coins = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        loot = new List<GameObject>();
        for (int i = 0; i < Random.Range(1,10); i++)
        {
            //GameObject coin = Instantiate(coinObject, transform);
            //coin.SetActive(false);
            //loot.Add(coin);
            coins++;
        }
    }
    private void OnEnable()
    {
        for (int i = 0; i < Random.Range(1, 10); i++)
        {
            //GameObject coin = Instantiate(coinObject, transform);
            //coin.SetActive(false);
            //loot.Add(coin);
            coins++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DropLoot()
    {
        transform.DetachChildren();
        foreach (var coin in loot)
        {
            coin.SetActive(true);
            
        }
    }
    public int GetCoins()
    {
        return coins;
    }
  
}
