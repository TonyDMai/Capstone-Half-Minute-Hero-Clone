using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ModifiedEvent();
/// <summary>
/// Class that will handle adding and removing additional stats from items, each item will implement the IModifier class which forces it to have the add value method.
/// This prevents our game from dying when items get unequipped or deleted
/// </summary>
[System.Serializable]
public class ModifiableInt
{

    [SerializeField]
    private int baseValue; // Base Value of Stat
    public int BaseValue { 
        get { return baseValue; }
        set { 
            baseValue = value;
            UpdateModifiedValue();
        }
    }

    public void setBaseValue(int _value) {
        baseValue = _value;
    }


    [SerializeField]
    private int modifiedValue;
    public int ModifiedValue { 
        get { return modifiedValue; }
        private set { modifiedValue = value; }
    }

    public List<IModifier> modifiers = new List<IModifier>();

    public event ModifiedEvent ValueModified;
    /// <summary>
    /// 
    /// Dont always have to pass event in.
    /// </summary>
    /// <param name="method"></param>
    public ModifiableInt(ModifiedEvent method = null) {
        modifiedValue = baseValue;
        if (method != null) {
            ValueModified += method;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="method"></param>
    public void RegisterModEvent(ModifiedEvent method) {
        ValueModified += method;
    }
    /// <summary>
    /// Removes the modifier from the list
    /// </summary>
    /// <param name="method"></param>
    public void UnregisterModEvent(ModifiedEvent method)
    {
        ValueModified -= method;
    }

    public void UpdateModifiedValue()
    {
        var valueToAdd = 0;
        for (int i = 0; i < modifiers.Count; i++)
        {
            modifiers[i].AddValue(ref valueToAdd);
        }
        modifiedValue = baseValue + valueToAdd;
        if (ValueModified != null) {
            ValueModified.Invoke();
        }
    }

    public void AddModifier(IModifier _modifier) {
        modifiers.Add(_modifier);
        UpdateModifiedValue();
    }

    public void RemoveModifier(IModifier _modifier)
    {
        modifiers.Remove(_modifier);
        UpdateModifiedValue();
    }
}
