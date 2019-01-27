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

    [Header("Countdowns")]
    public Countdown GameStartCountdown;
    public Countdown GameEndCountdown;

    [Header("Annuncements")]
    public GameObject Player1WinnerAnnuncement;
    public GameObject Player2WinnerAnnuncement;
    public GameObject DrawAnnuncement;

    /// <summary>
    /// Begins the game
    /// </summary>
    public void StartGame()
    {
        TimeLeft = new TimeSpan(0, 0, GameTimeSeconds);
        GameRunning = true;
        InvokeRepeating("CoTickTime", 0f, 1f);
    }

    /// <summary>
    /// Ends the game and announces the winner.
    /// </summary>
    public void EndGame()
    {
        GameRunning = false;

        //Stops the players
        FindObjectsOfType<BirdController>().ForEach(i => i.Move(Vector2.zero, 0));

        Invoke("AnnounceWinner", 2f);
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

    private void CoTickTime()
    {
        TimeLeft -= new TimeSpan(0, 0, seconds: 1);

        //Trigger TIME countdown
        if (TimeLeft == new TimeSpan(0, 0, 3))
        {
            Instantiate(GameEndCountdown);
        }
        else if (TimeLeft == new TimeSpan(0, 0, 0))
        {
            //Ends the game
            CancelInvoke();
            EndGame();
        }


        
    }

    private void AnnounceWinner()
    {
        

        IGrouping<int, Nest> winnerNests = FindObjectsOfType<Nest>()
            .OrderByDescending(i => i.TotalValue)
            .GroupBy(i => i.TotalValue)
            .First();

        if (winnerNests.Count() > 1)
        {
            Instantiate(DrawAnnuncement);
        }
        else
        {
            switch (winnerNests.Single().ID)
            {
                case 1:
                    Instantiate(Player1WinnerAnnuncement);
                    break;
                case 2:
                    Instantiate(Player2WinnerAnnuncement);
                    break;
            }
        }


        
    }

    private void Start()
    {
        GetComponent<AudioSource>().clip.LoadAudioData();
        GetComponent<AudioSource>().Play();
        Instantiate(GameStartCountdown);
    }

}
