using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Represents the main level manager. It includes among other things, the timer
/// </summary>
public class Level : MonoBehaviour
{
    [Header("Time limit")]
    [Description("The time limit of the game")]
    public int GameTimeSeconds;

    /// <summary>
    /// How much time is left on the clock
    /// </summary>
    public TimeSpan TimeLeft { get; set; }

    /// <summary>
    /// Is a game currently in progress
    /// </summary>
    public bool GameRunning { get; private set; }

    /// <summary>
    /// Begins the game
    /// </summary>
    public void StartGame()
    {
        TimeLeft = new TimeSpan(0, 0, GameTimeSeconds);
        GameRunning = true;
        StartCoroutine(CoTickTime());
    }

    /// <summary>
    /// Ends the game and announces the winner.
    /// </summary>
    public void EndGame()
    {
        GameRunning = false;
    }

    /// <summary>
    /// Gets the player with the given ID
    /// </summary>
    /// <param name="playerID">Player ID</param>
    /// <returns></returns>
    public BirdController GetPlayer(int playerID)
    {
        return FindObjectsOfType<BirdController>()
            .Where(i => i.ID == playerID)
            .SingleOrDefault();
    }

    private IEnumerator CoTickTime()
    {
        //Ticks the time until it reaches zero seconds
        while (TimeLeft > new TimeSpan(0, 0, 0))
        {
            TimeLeft -= new TimeSpan(0, 0, seconds: 1);
            yield return new WaitForSeconds(1);
        }

        //Ends the game
        EndGame();
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        DebugScreenDrawer.Enable("time", "Time: " + TimeLeft);
        DebugScreenDrawer.Enable("run", "InGame: " + GameRunning);
    }

}
