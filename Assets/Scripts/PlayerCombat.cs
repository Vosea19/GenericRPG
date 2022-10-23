using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Spell fireBall, iceCone, lightningCube;
    Camera playerCamera;
    float timer = 0;
    float lastCastTime = 0;
    public GameObject spellInCharging;
    private Projectile channelledProj;
    private int chargeSpellBaseDamage;
    private Vector3 chargeSpellBaseScale;
    private float lastChargeTime = 0;
    private float chargeInterval = 0.1f;
    private Animator animator;
    private bool castAnimComplete;
    GameManager objectPooler;
    

    
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = GameManager.Instance;
        player = gameObject.GetComponent<Player>();
        if (player == null)
        {
            throw new ArgumentException("PlayerCombat must have a reference to player");
        }
        animator = GetComponent<Animator>();
        playerCamera = Camera.main;
    }
    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            castAnimComplete = true;
        }
        else castAnimComplete = false;
        timer += Time.deltaTime;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (Input.GetKey(KeyCode.Q) && CastReady(fireBall))
        {
            
            if (fireBall.multipleProjectiles)
            {
                
                CastMultipleSpell("fireball", fireBall.manaCost, fireBall.distance, fireBall.numOfProjectiles);
            }
            else CastSpell("fireball", fireBall.manaCost, fireBall.distance);
        }
        if (Input.GetKey(KeyCode.W) && CastReady(iceCone))
        {
            if (iceCone.multipleProjectiles)
            {
                CastMultipleSpell("iceCone", iceCone.manaCost, iceCone.distance, iceCone.numOfProjectiles);
            }
            else CastSpell("iceCone", iceCone.manaCost, iceCone.distance);
        }
        if (Input.GetKeyDown(KeyCode.E) && CastReady(lightningCube))
        {
            spellInCharging = StartSpell("lightningCube", lightningCube.manaCost, lightningCube.distance);
        }
        if (Input.GetKey(KeyCode.E) && ChargeReady(lightningCube) && spellInCharging != null)
        {
            ChargeSpell();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            ReleaseSpell();
        }
        
    }
    private Vector3 GetMouseHit()
    { 
        Ray pointRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(pointRay, out RaycastHit point ,100f);
        Vector3 position = new Vector3(point.point.x, transform.position.y, point.point.z);
        transform.LookAt(position);
        return position;
    }

    private void CastSpell(string spellType, int spellCost, float spellDistance)
    {
        //animator.SetTrigger("Cast");
        if (!player.SpendMana(spellCost))
        {
            print("out of mana");
            return;
        }
        GameObject spell = objectPooler.PoolInstantiate(spellType, transform.position, Quaternion.identity);
        spell.GetComponent<Projectile>().startMove(GetMouseHit());
        lastCastTime = timer;
    }
    private GameObject StartSpell(string spellType, int spellCost, float spellDistance)
    {
        if (!player.SpendMana(spellCost))
        {

        }
        
       GameObject spell = objectPooler.PoolInstantiate(spellType, transform.position, Quaternion.identity);
        
        lastCastTime = timer;
        channelledProj = spell.GetComponent<Projectile>();
        //chargeSpellBaseScale = chargeSpell.transform.localScale;
        return spell;
        
    }
    private void ChargeSpell()
    {
        
        channelledProj.channelDamage += (int)Mathf.Ceil(chargeInterval * lightningCube.damage);
        spellInCharging.transform.localScale += (chargeInterval * chargeSpellBaseScale);
        spellInCharging.transform.position = transform.position;
        player.SpendMana( (int)Mathf.Ceil(chargeInterval * lightningCube.manaCost));
        lastChargeTime = timer;

    }
    private void ReleaseSpell()
    { 
        spellInCharging.GetComponent<Projectile>().startMove(GetMouseHit());
        spellInCharging = null;
        lastCastTime = timer;

    }
    private void CastMultipleSpell(string spellType, int spellCost, float spellDistance, int numOfSpells)
    {
        //animator.SetTrigger("Cast");
        if (!player.SpendMana(spellCost))
        {
            print("out of mana");
            return;
        }
        Vector3 position = GetMouseHit();
        float distanceScaler = Mathf.Clamp(Vector3.Distance(position,transform.position),1,5);
        int startVal;
        if (numOfSpells % 2 == 0)
        {
            startVal = -(numOfSpells / 2);
        }
        else
        {
            startVal = -((numOfSpells - 1) / 2);
        }
        float[] offsets = new float[numOfSpells - 1];
        for (int i = startVal; i < 0; i++)
        {
            offsets[i + (-startVal)] = i;
            
        }
        for (int i = 0; i < -startVal; i++)
        {
            offsets[i + (-startVal)] = -offsets[i];
        }
        
        for (int i = 0; i < offsets.Length; i++)
        {
           
               GameObject spell = objectPooler.PoolInstantiate(spellType, transform.position, Quaternion.identity);
            spell.SetActive(true);
            spell.GetComponent<Projectile>().startMove(position + transform.right * offsets[i] * distanceScaler);
            
        }
        if (numOfSpells % 2 != 0)
        {
            GameObject spell = objectPooler.PoolInstantiate(spellType, transform.position, Quaternion.identity);
            spell.SetActive(true);
            spell.GetComponent<Projectile>().startMove(position);
        }


        lastCastTime = timer;
    }
    private bool CastReady(Spell spell)
    {
        return (timer >= lastCastTime + 1f / spell.castTime);
    }
    private bool ChargeReady(Spell spell)
    {
        return (timer >= lastChargeTime + 1f / spell.castTime);
    }

}
