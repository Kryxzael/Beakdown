using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Scriptable object that represents the spawn rate of items
/// </summary>
[CreateAssetMenu(menuName = "Junk Spawn Rate Info")]
public class JunkSpawnRate : ScriptableObject
{
    [Description("The chance of this item being spawned")]
    [Range(0f, 1f)]
    public float SpawnChance;

    [Description("The item that will be spawned")]
    public Junk Item;
}
