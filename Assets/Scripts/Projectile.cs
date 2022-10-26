using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    Spell spell;
    public GameObject explosionSpell;
    private List<GameObject> hitEnemies;
    private Coroutine movement;
    private int chainsLeft;
    private ParticleSystem particle;
    private bool hasParticles;
    public int channelDamage;
    private void OnEnable()
    {
        chainsLeft = spell.numChains;
        channelDamage = spell.damage;
    }
    private void Awake()
    {

        hasParticles = TryGetComponent<ParticleSystem>(out particle);
        hitEnemies = new List<GameObject>();
        Collider collider = GetComponent<Collider>();
        if (spell.projectileMechanic != ProjectileMechanic.None)
        {
            collider.isTrigger = true;
        }
        else collider.isTrigger = false;
    }
    public void startMove(Vector3 point)
    {
        if (movement != null)
        {
            StopCoroutine(movement);
        }
        movement = StartCoroutine(ProjectileMove(point));
    }
    IEnumerator ProjectileMove(Vector3 point)
    {
        transform.LookAt(point);
        int steps = Mathf.FloorToInt(spell.distance / spell.speed);
        for (int i = 0; i < steps; i++)
        {
            transform.position += (transform.forward * spell.speed);
            yield return new WaitForSeconds(0.02f);
        }
        transform.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        TargetInteraction(collision.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        TargetInteraction(other.gameObject);
    }

    public int SpellHit()
    {
        int damage = 0; ;
        if (hasParticles)
        {
            particle.Play();
        }
        /*
        if (explosionSpell != null)
        {
            GameObject explosion = Instantiate(explosionSpell, transform.position, Quaternion.identity);
            explosion.GetComponent<Explosion>().StartExplode(spell.chainDistance);
        }
        */
        switch (spell.projectileMechanic)
        {
            case ProjectileMechanic.None:
                //transform.gameObject.SetActive(false);
                damage = spell.damage;
                break;
            case ProjectileMechanic.Pierce:
                damage = spell.damage;
                break;
            case ProjectileMechanic.Chain:
                damage = spell.damage;
                Chain();
                break;
            case ProjectileMechanic.Channel:
                damage = channelDamage;
                break;
            default:
                break;
        }
        return damage;

    }
    private void Chain()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, spell.chainDistance);
        if (colliders.Length <= 0)
        {
            return;
        }
        List<Collider> newColliders = new List<Collider>();
        foreach (var collider in colliders)
        {
            newColliders.Add(collider);
        }
        newColliders.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position)));
        foreach (var collider in newColliders)
        {
            if (collider.TryGetComponent(out Npc nextTarget) && !collider.CompareTag("Player") && !hitEnemies.Contains(collider.gameObject))
            {
                if (chainsLeft <= 0)
                {
                    transform.gameObject.SetActive(false);
                    return;
                }
                startMove(collider.transform.position);
                chainsLeft--;
                Debug.Log(chainsLeft);
                break;
            }
        }
    }
    private void TargetInteraction(GameObject target)
    {
        if (target.transform.TryGetComponent(out Npc npc))
        {

            hitEnemies.Add(target);
            npc.Health.TakeDamage(SpellHit());
        }
    }
}
