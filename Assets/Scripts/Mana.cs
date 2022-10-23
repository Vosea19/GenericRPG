using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public int maxMana;
    public int currentMana;
    public int manaRegen;
    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana;
        manaRegen = 5;
        StartCoroutine(ManaRegen());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ManaRegen()
    {
        while (true)
        {
            if (currentMana < maxMana)
            {
                currentMana += manaRegen;
                if (currentMana > maxMana)
                {
                    currentMana = maxMana;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
