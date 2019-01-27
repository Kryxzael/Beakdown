using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// This value is set randomly every second and determines whether or not the item can be grabbed if inside a nest
    /// </summary>
    public bool CanGrabInNest { get; private set; }

    [Description("What is the likelyhood if this item being grabbable when inside a nest?")]
    public float ChanceOfGrabInNest = 0.2f;

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

    private void Start()
    {
        InvokeRepeating(nameof(SetGrabbableState), 0f, 1f);
    }

    private void Update()
    {
        if (Grabbed)
        {
            transform.position = Grabber.GetComponent<Collider2D>()?.bounds.center ?? transform.position;
        }
    }

    private void SetGrabbableState()
    {
        CanGrabInNest = UnityEngine.Random.value <= ChanceOfGrabInNest;
    }
}