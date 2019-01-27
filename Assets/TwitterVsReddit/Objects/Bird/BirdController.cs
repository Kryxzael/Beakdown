using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// A script that allows for controlling of a player
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviourWithID
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

    [Header("Stunning")]
    public bool Stunned;
    public float StunTime = 0.1f;
    public float StunPushForce;



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
        DebugScreenDrawer.Enable("spd", "SPD: " + _rb.velocity);

        /*
         *Basic movement
         */

        //Do not move if game is finished or you are stunned
        if (!this.GetGameManager().GetComponent<Level>().GameRunning || Stunned)
        {
            return;
        }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Spiker spiker = collision.GetComponent<Spiker>();

        //Collision with another beak
        if (spiker != null && spiker.CurrentState == Spiker.State.Grab)
        {
            StartCoroutine("CoStun", CommonExtensions.VectorFromDegree(transform.Position2D().RealAngleBetween(collision.transform.position)));
        }
    }

    private IEnumerator CoStun(Vector2 pushDirection)
    {
        if (Random.value <= 1 / 3f)
        {
            GetComponentInChildren<Grabber>()?.ReleaseAll();
            if (GetComponentInChildren<Spiker>() != null)
            {
                GetComponentInChildren<Spiker>().CurrentState = Spiker.State.None;
            }
        }


        Debug.Log(StunPushForce);
        _rb.velocity = pushDirection * StunPushForce * -Vector2.one;
        StartCoroutine("CoShake");
        Stunned = true;
        yield return new WaitForSeconds(StunTime);
        Stunned = false;
        StopCoroutine("CoShake");

    }

    private IEnumerator CoShake()
    {
        float originalRotation = transform.rotation.eulerAngles.z;
        while (true)
        {
            transform.SetEuler2D(Random.Range(-20, 20) + originalRotation);
            yield return new WaitForEndOfFrame();
        }
    }
}
