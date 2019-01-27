using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviourWithID
{

    private Text _text;
    private Nest _target;

    private void Start()
    {
        _text = GetComponent<Text>();
        _target = FindObjectsOfType<Nest>()
            .Single(i => i.ID == this.ID);
    }

    private void Update()
    {
        _text.text = _target.TotalValue.ToString("0000");
    }
}
