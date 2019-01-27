using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Grabber))]
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
    /// The current animation state of the player
    /// </summary>
    public State CurrentState;

    /// <summary>
    /// The current cooldown of the player. The player will automaticly enter the carry or none state when this value is zero
    /// </summary>
    private float _cooldown;

    /// <summary>
    /// The time the player has been charging for
    /// </summary>
    private float _chargeTime;

    private bool _shaking;

    private Grabber _grabber;

    private void Awake()
    {
        _grabber = GetComponent<Grabber>();
    }

    private void Update()
    {
        if (!this.GetGameManager().GetComponent<Level>().GameRunning)
        {
            return;
        }

        switch (CurrentState)
        {
            case State.None:
                if (Input.GetButtonDown(SpikeAxis))
                {
                    _chargeTime = 0;
                    CurrentState = State.Charging;
                }
                break;
            case State.Charging:
                if (!Input.GetButton(SpikeAxis))
                {
                    _cooldown = CooldownTime;
                    CurrentState = State.Grab;
                }

                _chargeTime += Time.deltaTime;
                transform.localScale = transform.localScale.SetY(BeakDrawbackAnimation.Evaluate(_chargeTime / MaxChargeTime));

                if (!_shaking && _chargeTime >= MaxChargeTime)
                {
                    _shaking = true;
                    GetComponentInParent<BirdController>().StartCoroutine("CoShake");
                }
                break;
            case State.Grab:
                _shaking = false;
                GetComponentInParent<BirdController>().StopCoroutine("CoShake");

                if (_cooldown <= 0)
                {
                    if (_grabber.IsGrabbing)
                    {
                        CurrentState = State.Carry;
                    }
                    else
                    {
                        CurrentState = State.None;
                    }
                }

                //Grab objects
                _grabber.GrabOnHitbox();

                //Stretches beak
                float curveResult = ReleaseCurve.Evaluate(1f - (_cooldown / CooldownTime));
                float lerp = Mathf.Lerp(LowRange, HighRange, Mathf.Clamp(_chargeTime / MaxChargeTime, 0f, 1f));

                transform.localScale = transform.localScale.SetY(1 + (curveResult * lerp));


                _cooldown -= Time.deltaTime;

                break;
            case State.Carry:
                if (Input.GetButtonDown(SpikeAxis))
                {
                    _grabber.ReleaseAll();
                    CurrentState = State.None;
                }
                break;
        }
    }


    /// <summary>
    /// Represents a Spiker's animation states
    /// </summary>
    public enum State
    {
        /// <summary>
        /// No items are being carried
        /// </summary>
        None = 0,

        /// <summary>
        /// The player is charging their beak
        /// </summary>
        Charging = 1,

        /// <summary>
        /// The player has released their beak
        /// </summary>
        Grab = 2,

        /// <summary>
        /// The player is carrying something
        /// </summary>
        Carry = 3,

        /// <summary>
        /// The player is currently releasing an item
        /// </summary>
        Release = 4
    }
}