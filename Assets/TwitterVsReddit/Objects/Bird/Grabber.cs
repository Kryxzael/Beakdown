using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Allows an object to grab a grabbale object by childing it
/// </summary>
public class Grabber : MonoBehaviour
{
    /// <summary>
    /// Is the grabber currently grabbing something?
    /// </summary>
    public bool IsGrabbing
    {
        get => GrabbedItems.Any();
    }

    /// <summary>
    /// Houses all the items that are currently being grabbed
    /// </summary>
    public List<Grabbable> GrabbedItems = new List<Grabbable>();

    /// <summary>
    /// The collider of this grabber. It can be null
    /// </summary>
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Grabs an item, regardless of how far away it is
    /// </summary>
    /// <param name="item"></param>
    public void Grab(Grabbable item)
    {
        item.Grabber = this;
        GrabbedItems.Add(item);

        item.transform.parent = transform;
    }

    /// <summary>
    /// Grabs any grabbable object within the grabber's hitbox 
    /// </summary>
    public void GrabOnHitbox()
    {
        //Cannot run this function on colliderless grabbers
        if (_collider == null)
        {
            Debug.LogWarning("Attempted to run GrabOnHitbox() on colliderless grabber");
        }

        //Finds every grabbable that is within the hitbox of the grabber
        foreach (Grabbable i in FindObjectsOfType<Grabbable>())
        {
            if ((i.GetComponent<Collider2D>()?.bounds.Intersects(_collider.bounds)) == true)
            {
                Grab(i);
            }
        }
    }

    /// <summary>
    /// Releases an item
    /// </summary>
    /// <param name="item"></param>
    public void Release(Grabbable item)
    {
        //Item was not grabbed by this grabber. Abort
        if (item.Grabber != this)
        {
            return;
        }

        item.Grabber = null;
        item.transform.parent = null;

        GrabbedItems.Remove(item);
    }

    /// <summary>
    /// Releases every item grabbed by the grabber
    /// </summary>
    public void ReleaseAll()
    {
        while (GrabbedItems.Any())
        {
            Release(GrabbedItems[0]);
        }
    }
}
