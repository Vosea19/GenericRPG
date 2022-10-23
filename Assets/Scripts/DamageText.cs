using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    
    IEnumerator Move()
    {
        float speed = 3f;
        Vector3 dir = Vector3.up;
        for (int i = 0; i < 50; i++)
        {
            transform.position += (speed * Time.deltaTime * dir);

            yield return new WaitForEndOfFrame();
        }
        Destroy(transform.gameObject);
    }
}
