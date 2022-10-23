using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpdater : MonoBehaviour
{
    public GameObject player;
    private Image healthBar;
    private Text healthText;
    private Player playerHealth;
    private Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
        healthBar = GetComponent<Image>();
        healthText = transform.GetChild(0).GetComponent<Text>();
        playerHealth = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {            
        healthBar.fillAmount = (float)playerHealth.GetHealth() / (float)playerHealth.GetMaxHealth();
        healthText.text = playerHealth.GetHealth().ToString() + "/" + playerHealth.GetMaxHealth().ToString() ;
    }
  
}
