using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Junk : MonoBehaviour
{
    [Header("Stats")]
    [Description("The value in points this piece of junk will provide")]
    public int Value;

    [Description("Multiplier applied to the player's speed when the object is being held")]
    public float SpeedMultipler;
}
