using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeDisplay : MonoBehaviour
{
    private Level _manager;
    private Text _text;

    private void Start()
    {
        _manager = this.GetGameManager().GetComponent<Level>();
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _text.text = _manager.TimeLeft.ToString("m':'ss");
    }
}
