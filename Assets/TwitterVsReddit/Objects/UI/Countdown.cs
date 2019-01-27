using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public GameObject Three;
    public GameObject Two;
    public GameObject One;
    public GameObject Zero;

    private IEnumerator Start()
    {
        GameObject currentNumber;

        currentNumber = Instantiate(Three);
        yield return new WaitForSeconds(1f);
        Destroy(currentNumber);

        currentNumber = Instantiate(Two);
        yield return new WaitForSeconds(1f);
        Destroy(currentNumber);

        currentNumber = Instantiate(One);
        yield return new WaitForSeconds(1f);
        Destroy(currentNumber);

        currentNumber = Instantiate(Zero);
    }
}