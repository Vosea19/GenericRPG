using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Transform explosionTransform;
    private Damage damage;
    // Start is called before the first frame update
    void Awake()
    {
        explosionTransform = transform;
        damage = GetComponent<Damage>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (other.TryGetComponent<IDamageable>(out IDamageable health))
        {
            health.TakeDamage(damage.damage);
        }
        
    }
}
