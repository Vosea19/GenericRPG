using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Transform explosionTransform;
    void Awake()
    {
        explosionTransform = transform;
    }
    public void StartExplode(float radius)
    {
        StartCoroutine(Explode(radius));
    }
    IEnumerator Explode(float radius)
    {
        for (int i = 0; i < (int)(radius); i++)
        {
            explosionTransform.localScale += (Vector3.one * 2);
            yield return new WaitForEndOfFrame();
        }
        Destroy(explosionTransform.gameObject);     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Npc>(out Npc health))
        {
            health.TakeDamage(10);
        }
        
    }
}
