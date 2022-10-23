using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTo : MonoBehaviour
{
    private Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerCamera.transform);
    }
}
