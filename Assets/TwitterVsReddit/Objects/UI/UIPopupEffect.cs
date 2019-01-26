using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Makes a UI effect look fancy. This script is hard coded AF cause I'm tired
/// </summary>
public class UIPopupEffect : MonoBehaviour
{
    private Vector3 _realScale;
    private Vector3 _realRotation;

    private Vector3 _initScale = new Vector3(0, 0, 0);
    private Vector3 _initRotation = new Vector3(0, 0, -100);

    private IEnumerator Start()
    {
        Debug.Log("Start");

        _realScale = transform.localScale;
        _realRotation = transform.eulerAngles;

        for (float i = 0; i < 1; i += 0.05f)
        {
            transform.localScale = Vector3.Lerp(_initRotation, _realScale, i);
            transform.SetEuler(Vector3.Lerp(_initRotation, _realRotation, i));

            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(2);

        SpriteRenderer spr = GetComponent<SpriteRenderer>();

        for (float i = 1; i >= 0; i -= 0.05f)
        {
            spr.color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.01f);
        }

        Destroy(gameObject);
    }
}
