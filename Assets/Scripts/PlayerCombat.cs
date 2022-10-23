using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    Spell fireBall,iceCone,lightningCube;
    [SerializeField]
    Player player;
    Camera playerCamera;
    [SerializeField]
    GameObject fireBallSpell,iceConeSpell,chargeSpell;
    float timer = 0;
    float lastCastTime = 0;
    Mana mana;
    public GameObject spellInCharging;
    private Projectile channelledProj;
    private int chargeSpellBaseDamage;
    private Vector3 chargeSpellBaseScale;
    private float lastChargeTime = 0;
    private float chargeInterval = 0.1f;
    private List<GameObject> fireBallList;
    private List<GameObject> iceConeList;
    private List<GameObject> lightningCubeList;
    private Animator animator;
    private bool castAnimComplete;
    private enum Spells {fireball = 0, iceCone = 1, lightningCube = 2};

    
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            throw new ArgumentException("PlayerCombat must have a reference to player");
        }
        animator = GetComponent<Animator>();
        fireBallList = new List<GameObject>();
        iceConeList = new List<GameObject>();
        lightningCubeList = new List<GameObject>();
        playerCamera = Camera.main;
        mana = GetComponent<Mana>();
        chargeSpellBaseDamage = lightningCube.damage;
        for (int i = 0; i < 25; i++)
        {
            PoolInstantiate(Spells.fireball,transform.position,transform.rotation).SetActive(false);
            PoolInstantiate(Spells.iceCone, transform.position, transform.rotation).SetActive(false);
            PoolInstantiate(Spells.lightningCube, transform.position, transform.rotation).SetActive(false);
        }
       
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
                CastMultipleSpell(Spells.fireball, fireBall.manaCost, fireBall.distance, fireBall.numOfProjectiles);
            }
            else CastSpell(Spells.fireball, fireBall.manaCost, fireBall.distance);
        }
        if (Input.GetKey(KeyCode.W) && CastReady(iceCone))
        {
            if (iceCone.multipleProjectiles)
            {
                CastMultipleSpell(Spells.iceCone, iceCone.manaCost, iceCone.distance, iceCone.numOfProjectiles);
            }
            else CastSpell(Spells.iceCone, iceCone.manaCost, iceCone.distance);
        }
        if (Input.GetKeyDown(KeyCode.E) && CastReady(lightningCube))
        {
            spellInCharging = StartSpell(Spells.lightningCube, lightningCube.manaCost, lightningCube.distance);
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

    private void CastSpell(Spells spellType, int spellCost, float spellDistance)
    {
        animator.SetTrigger("Cast");
        GameObject spell = PoolInstantiate(spellType, transform.position, Quaternion.identity);
        mana.currentMana -= spellCost;
        spell.GetComponent<Projectile>().startMove(GetMouseHit());
        lastCastTime = timer;
    }
    private GameObject StartSpell(Spells spellType, int spellCost, float spellDistance)
    {
        GameObject spell = PoolInstantiate(spellType, transform.position, Quaternion.identity);
        mana.currentMana -= spellCost;
        lastCastTime = timer;
        channelledProj = spell.GetComponent<Projectile>();
        chargeSpellBaseScale = chargeSpell.transform.localScale;
        return spell;
    }
    private void ChargeSpell()
    {
        
        channelledProj.channelDamage += (int)Mathf.Ceil(chargeInterval * lightningCube.damage);
        spellInCharging.transform.localScale += (chargeInterval * chargeSpellBaseScale);
        spellInCharging.transform.position = transform.position;
        mana.currentMana -= (int)Mathf.Ceil(chargeInterval * lightningCube.manaCost);
        lastChargeTime = timer;

    }
    private void ReleaseSpell()
    { 
        spellInCharging.GetComponent<Projectile>().startMove(GetMouseHit());
        spellInCharging = null;
        lastCastTime = timer;

    }
    private void CastMultipleSpell(Spells spellType, int spellCost, float spellDistance, int numOfSpells)
    {
        animator.SetTrigger("Cast");
            
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
           
                GameObject spell = PoolInstantiate(spellType, transform.position, Quaternion.identity);
                spell.GetComponent<Projectile>().startMove(position + transform.right * offsets[i] * distanceScaler);
            
        }
        if (numOfSpells % 2 != 0)
        {
            GameObject spell = PoolInstantiate(spellType, transform.position, Quaternion.identity);
            spell.GetComponent<Projectile>().startMove(position);
        }
        
        mana.currentMana -= spellCost;
        lastCastTime = timer;
    }
    private GameObject PoolInstantiate(Spells spell, Vector3 position, Quaternion rotation)
    {
        List<GameObject> spellList = null;
        GameObject spellPrefab = null;
        switch (spell)
        {
            case Spells.fireball:
                spellList = fireBallList;
                spellPrefab = fireBallSpell;
                break;
            case Spells.iceCone:
                spellList = iceConeList;
                spellPrefab = iceConeSpell;
                break;
            case Spells.lightningCube:
                spellList = lightningCubeList;
                spellPrefab = chargeSpell;
                break;
        }
        foreach (var item in spellList)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = position;
                item.transform.localScale = spellPrefab.transform.localScale;
                return item;
            }
        }
        GameObject newSpell = Instantiate(spellPrefab, position, rotation);
        spellList.Add(newSpell);
        return newSpell;       
    }
    private bool CastReady(Spell spell)
    {
        return (timer >= lastCastTime + 1f / spell.castTime && mana.currentMana >= spell.manaCost);
    }
    private bool ChargeReady(Spell spell)
    {
        return (timer >= lastChargeTime + 1f / spell.castTime && mana.currentMana >= spell.manaCost);
    }

}
