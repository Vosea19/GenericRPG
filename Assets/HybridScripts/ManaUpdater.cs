using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaUpdater : MonoBehaviour
{
    public GameObject player;
    private Image ManaBar;
    private Text ManaText;
    private Mana playerMana;
    private Camera playerCamera;
    
    void Start()
    {
        playerCamera = Camera.main;
        ManaBar = GetComponent<Image>();
        ManaText = transform.GetChild(0).GetComponent<Text>();
        playerMana = player.GetComponent<Mana>();
        playerMana.OnManaChange += UpdateMana;
    }


    private void UpdateMana(object sender, Mana.OnManaChangeEventArgs e)
    {
        ManaBar.fillAmount = (float)playerMana.GetMana() / (float)playerMana.GetMaxMana();
        ManaText.text = playerMana.GetMana().ToString() + "/" + playerMana.GetMaxMana().ToString();
    }
}
