using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Nest : MonoBehaviourWithID
{

    private Collider2D _collider;

    /// <summary>
    /// Gets every piece of junk in the nest
    /// </summary>
    public IEnumerable<Junk> JunkInNest
    {
        //Get every piece of junk...
        get => FindObjectsOfType<Junk>()

            //...that is not currently grabbed...
            .Where(i => i.GetComponent<Grabbable>()?.Grabbed != true)

            //...that is in the trigger of the nest
            .Where(i => i.GetComponent<Collider2D>()?.bounds.Intersects(_collider.bounds) == true);
    }

    /// <summary>
    /// The total score of the nest, obtained by adding the value of every item in the nest
    /// </summary>
    public int TotalValue
    {
        get => JunkInNest.Sum(i => i.Value);
    }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
}