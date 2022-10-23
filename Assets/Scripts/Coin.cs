using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody rb;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveTowardsPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MoveTowardsPlayer()
    {
        while (true)
        {
            rb.MovePosition(Vector3.Lerp(transform.position, player.transform.position,.01f));
            rb.MovePosition(player.transform.position);
            yield return new WaitForEndOfFrame();

        }
        
    }
}
