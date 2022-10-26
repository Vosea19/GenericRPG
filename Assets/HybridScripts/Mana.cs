using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Mana : MonoBehaviour
{
    private int maxMana;
    private int mana;

    public event EventHandler<OnManaChangeEventArgs> OnManaChange;
    public class OnManaChangeEventArgs : EventArgs
    {
        public GameObject passedObject;
    }
    public void SetMana(int newMana)
    {
        if (newMana <= maxMana)
        {
            mana = newMana;
        }
        if (newMana <= 0)
        {
            mana = 0;
        }
        OnManaChange?.Invoke(this, new OnManaChangeEventArgs { passedObject = this.gameObject });
    }
    public int GetMana()
    {
        return mana;
    }
    public void SetMaxMana(int newMaxMana)
    {
        if (newMaxMana >= 0)
        {
            maxMana = newMaxMana;
        }
        else
        {
            throw new ArgumentException("Max Mana Cannot be set to a negative value");
        }

    }
    public int GetMaxMana()
    {
        return maxMana;
    }
    public bool SpendMana(int manaToSpend)
    {
        if (manaToSpend > mana)
        {
            return false;
        }
        else
        {
            SetMana(mana - manaToSpend);
            return true;

        }

    }
}
