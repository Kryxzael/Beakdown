using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Makes a sprite fade in when it spawns
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class FadeInSprite : MonoBehaviour
{
    [Description("The time it takes for the sprite to fade in (in seconds (eh. don't count on it))")]
    public float FadeInTime = 1f;

    private IEnumerator Start()
    {        
        SpriteRenderer spr = GetComponent<SpriteRenderer>();

        for (float i = 0; i < 1f; i += 0.01f)
        {
            spr.color = new Color(1f, 1f, 1f, i);

            yield return new WaitForSeconds(FadeInTime / 1000f);
        }
    }
}
