using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// A script that allows for controlling of a player
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviour
{
    [Header("Directions")]
    [Description("The input axis that controls horizontal movement")]
    public string HorizontalAxis;

    [Description("The input axis that controls vertical movement")]
    public string VerticalAxis;

    [Header("Speeds and Timing")]
    [Description("The base speed of the player")]
    public float MoveSpeed;

    [Header("Status")]
    public Direction8 Direction; 

    /// <summary>
    /// The rigidbody of the player
    /// </summary>
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /*
         *Basic movement
         */

        //Get movement axis
        Vector2 axis = new Vector2(
            x: Input.GetAxis(HorizontalAxis),
            y: Input.GetAxis(VerticalAxis)
        );

        //TODO Store axis as Direction8

        //Apply movement to player
        Move(axis, MoveSpeed);
    }

    /// <summary>
    /// Makes the player move
    /// </summary>
    /// <param name="direction">Direction to move in</param>
    /// <param name="speed">Speed multipler</param>
    public void Move(Vector2 direction, float speed)
    {
        //Updates the velocity of the rigidbody
        _rb.velocity = direction * speed;
    }
}
