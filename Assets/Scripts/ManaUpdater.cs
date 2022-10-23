using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaUpdater : MonoBehaviour
{
    public GameObject player;
    private Image manaBar;
    private Text manaText;
    private Mana playerMana;
    // Start is called before the first frame update
    void Start()
    {
        manaBar = GetComponent<Image>();
        manaText = transform.GetChild(0).GetComponent<Text>();
        playerMana = player.GetComponent<Mana>();
    }

    // Update is called once per frame
    void Update()
    {
        manaBar.fillAmount = (float)playerMana.currentMana / (float)playerMana.maxMana;
        manaText.text = playerMana.currentMana.ToString() + "/" + playerMana.maxMana.ToString();
    }
}
