using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryVisualizer : MonoBehaviour
{
    public GameObject slot;
    private List<GameObject> slotList;
    private Camera playerCamera;
    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GameObject.Find("Player").TryGetComponent<Inventory>(out inventory));
        playerCamera = Camera.main;
        slotList = new List<GameObject>(32);
        GameObject goldSlot = GameObject.Find("GoldSlot");
        goldSlot.transform.GetChild(1).GetComponent<Text>().text = inventory.coinCount.ToString();
        if (slotList.Count <32)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    GameObject newSlot = Instantiate(slot, transform);
                    newSlot.transform.position = new Vector3(120 * j, -120 * i, 0) + transform.position;
                    newSlot.transform.GetChild(0).GetComponent<Text>().text = ((i * 8) + (j)).ToString();
                    slotList.Add(newSlot);
                }

            }
        }
        int z = 0;
        //Debug.Log(slotList.Count);
        
        foreach (var item in inventory.inventory)
        {
            Image slotImage = slotList[z].transform.GetChild(1).GetComponent<Image>();
            slotImage.sprite = item.GetComponent<Image>().sprite;
            slotImage.enabled = true;
            //item.transform.SetParent(slotList[z].transform);
            //item.transform.localPosition = (new Vector3(0,0,1));
            //item.transform.localScale *= 500;
            //item.transform.GetComponent<Coin>().enabled = false;
            //item.SetActive(true);
            //Instantiate(item,slotList[z].transform);
            z++;
        }
        
        
    }
  

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit[] items = GetMouseHit();
            //items[0].transform.position = Input.mousePosition;

        }
        
    }
    private RaycastHit[] GetMouseHit()
    {
        Vector3 mousePos = Input.mousePosition;
        playerCamera.ScreenToWorldPoint(mousePos);
        Ray pointRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit[] pointList = Physics.RaycastAll(pointRay, 100f);
        return pointList;
    }
    public void HideItems()
    {
        foreach (var item in slotList)
        {
            item.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
