using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Direction4
{
    Up = 0,
    Right = 2,
    Down = 4,
    Left = 6,
}

public enum Direction8
{
    Up = 0,
    UpRight = 1,
    Right = 2,
    DownRight = 3,
    Down = 4,
    DownLeft = 5,
    Left = 6,
    UpLeft = 7
}

public static class DirectionExtensions
{
    /// <summary>
    /// Converts this Direction8 into a Direction4
    /// </summary>
    /// <param name="dir8"></param>
    /// <returns></returns>
    public static Direction4 ToDirection4(this Direction8 dir8)
    {
        //Argument is not is a compound direction.
        if ((int)dir8 % 2 == 1)
        {
            throw new ArgumentException("Cannot convert direction " + dir8 + " to cardinal");
        }

        return (Direction4)dir8;
    }

    /// <summary>
    /// Converts the Direction4 into a Direction8
    /// </summary>
    /// <param name="dir4"></param>
    /// <returns></returns>
    public static Direction8 ToDirection8(this Direction4 dir4)
    {
        return (Direction8)dir4;
    }

    /// <summary>
    /// Rotates the direction a set amount of times
    /// </summary>
    /// <param name="dir4"></param>
    public static Direction4 Rotate(this Direction4 dir4, int times = 1)
    {
        if (times >= 0)
        {
            return (Direction4)(((int)dir4 + times * 2) % 8);
        }

        throw new ArgumentException("Argument must be greater than or equal to zero");
    }

    /// <summary>
    /// Rotates the direction a set amount of times
    /// </summary>
    /// <param name="dir8"></param>
    public static Direction8 Rotate(this Direction8 dir8, int times)
    {
        if (times >= 0)
        {
            return (Direction8)(((int)dir8 + times) % 8);
        }

        throw new ArgumentException("Argument must be greater than or equal to zero");
    }

    /// <summary>
    /// Flips the direction 180 degrees
    /// </summary>
    /// <param name="dir8"></param>
    /// <returns></returns>
    public static Direction4 Flip(this Direction4 dir4)
    {
        return dir4.Rotate(2);
    }

    /// <summary>
    /// Flips the direction 180 degrees
    /// </summary>
    /// <param name="dir8"></param>
    /// <returns></returns>
    public static Direction8 Flip(this Direction8 dir8)
    {
        return dir8.Rotate(4);
    }
}