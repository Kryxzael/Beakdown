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

    [Header("Commands")]
    [Description("The input axis that makes the player grab/release items")]
    public string GrabReleaseAxis;

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

        //Store direction of the player
        if (axis != Vector2.zero)
        {
            Direction = DirectionExtensions.Construct(axis.x, axis.y);
        }
        

        //Apply movement to player
        Move(axis, MoveSpeed);

        //Rotate the player
        transform.SetEuler2D(CommonExtensions.RealAngleBetween(transform.Position2D(), transform.Position2D() + Direction.ToVector2()));

        /*
         * Grabbing
         */
        if (Input.GetButtonDown(GrabReleaseAxis))
        {
            Grabber grabber = GetComponentInChildren<Grabber>();

            if (grabber.IsGrabbing)
            {
                grabber.ReleaseAll();
            }
            else
            {
                grabber.GrabOnHitbox();
            }
        }
        
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.Position2D(), transform.Position2D() + Direction.ToVector2());
    }
}
