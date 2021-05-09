using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A interface that will assist in adding stats for items
/// </summary>
public interface IModifier
{
    /// <summary>
    /// Add extra value from buffs to stat value
    /// </summary>
    /// <param name="baseValue"></param>
    void AddValue(ref int baseValue);
}
