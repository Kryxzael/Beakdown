using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Represents a game object with an ID
/// </summary>
public abstract class MonoBehaviourWithID : MonoBehaviour
{
    [Header("ID")]
    [Description("The game object's numeric ID")]
    public int ID;
}
