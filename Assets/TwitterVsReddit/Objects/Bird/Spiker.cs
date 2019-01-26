using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Spiker : MonoBehaviour
{
    [Header("Controls")]
    [Description("The input axis that controls the spiking")]
    public string SpikeAxis;

    [Description("The time (in seconds) the player must hold the charge button in order to jump")]
    public float MaxChargeTime;

    [Description("The minimum time (in seconds) between each attack")]
    public float CooldownTime;

    [Header("Spike Range")]
    [Description("The lowest distance the beak can travel")]
    public float LowRange;

    [Description("The highest distance the beak can travel")]
    public float HighRange;

    [Header("Curves")]
    [Description("The animation curve that scales the beak when charging up")]
    public AnimationCurve BeakDrawbackAnimation;
    [Description("The animation curve that scales the beak when releasing the beak")]
    public AnimationCurve ReleaseCurve;

    /// <summary>
    /// Is the player currently holding the charge button?
    /// </summary>
    public bool Charging { get; private set; }

    /// <summary>
    /// How many seconds have the player been holding the charge button for. This counter resets every time the player presses the charge button
    /// </summary>
    private float _beakChargeTime;

    /// <summary>
    /// How many seconds until the player can attack again
    /// </summary>
    private float _cooldown;

    /// <summary>
    /// Begin charging your beak
    /// </summary>
    public void ChargeBeak()
    {
        //Reset break charge time
        _beakChargeTime = 0;
        Charging = true;
    }

    /// <summary>
    /// Release the beak
    /// </summary>
    public void ReleaseBeak()
    {
        Charging = false;
        _cooldown = CooldownTime;
    }

    private void Update()
    {
        /*
         * Controls
         */

        if (Input.GetButton(SpikeAxis) && !Charging && _cooldown <= 0)
        {
            ChargeBeak();
        }
        else if (!Input.GetButton(SpikeAxis) && Charging)
        {
            ReleaseBeak();
        }

        if (Charging)
        {
            _beakChargeTime += Time.deltaTime;
            transform.localScale = transform.localScale.SetY(BeakDrawbackAnimation.Evaluate(_beakChargeTime / MaxChargeTime));
        }
        else
        {
            _cooldown = Math.Max(0, _cooldown - Time.deltaTime);
            transform.localScale = transform.localScale.SetY(1 + ReleaseCurve.Evaluate(1f - (_cooldown / CooldownTime)));
        }
    }
}