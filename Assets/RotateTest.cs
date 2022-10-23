using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    private float yDegree = 45f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotate()); 
    }

 
    IEnumerator Rotate()
    {
        while (true)
        {
            for (int i = 0; i < yDegree; i++)
            {
                transform.Rotate(0, 1, 0);
                yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < yDegree * 2; i++)
            {
                transform.Rotate(0, -1, 0);
                yield return new WaitForEndOfFrame();
            }
            
            for (int i = 0; i < yDegree; i++)
            {
                transform.Rotate(0, 1, 0);
                yield return new WaitForEndOfFrame();
            }

        }
        
    }
}
