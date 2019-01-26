using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Represents an object that can be grabbed by a grabber
/// </summary>
public class Grabbable : MonoBehaviour
{
    /// <summary>
    /// Has this object been grabbed
    /// </summary>
    public bool Grabbed
    {
        get => Grabber != null;
    }

    /// <summary>
    /// What bird has grabbed this object
    /// </summary>
    public Grabber Grabber { get; set; }
}