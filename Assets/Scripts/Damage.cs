using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        if (damage == 0)
        {
            damage = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
