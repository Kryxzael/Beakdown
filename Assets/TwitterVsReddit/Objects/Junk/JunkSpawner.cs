using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A spawner that spawns junk in the level
/// </summary>
public class JunkSpawner : MonoBehaviour
{
    [Header("Range")]
    [Description("The boundary of the spawner. Items will not be spawned outside this boundary")]
    public Bounds Bounds;

    [Header("Timing")]
    [Description("The minimum amount of seconds between each wave of items")]
    public float MinSpawnFrequency;

    [Description("The maximum amount of seconds between each wave of items")]
    public float MaxSpawnFrequency;

    [Header("Quantitiy")]
    [Description("The minimum amount of items that can spawn during a wave (except the first one)")]
    public int MinItemsPerWave;

    [Description("The maximum amount of items that can spawn during a wave (except the first one)")]
    public int MaxItemsPerWave;

    [Header("Initial Quantity")]
    [Description("The minimum amount of items that can spawn on the first wave")]
    public int MinInitialItems;

    [Description("The maximum amount of items that can spawn on the first wave")]
    public int MaxInitialItems;

    [Header("Random Number Generator")]
    [Description("The seed that will be used for this level. The same seed will always generate the same level")]
    public int Seed;

    [Description("If true, the seed will be randomized on awake")]
    public bool UseRandomSeed;

    [Header("Exclusion")]
    [Description("The layer whose hitboxes will act as a no-spawn zone")]
    public string ExclustionZoneLayer;

    [Header("Junk")]
    [Description("The spawn rate info for this generator")]
    public JunkSpawnRate[] SpawnInfo;

    private System.Random _rng;

    public JunkSpawner()
    {
        //Set up the RNG

        if (UseRandomSeed)
        {
            _rng = new System.Random();
        }
        else
        {
            _rng = new System.Random(Seed);
        }
    }

    private IEnumerator Start()
    {
        //Generate the first wave
        GenerateAndSpawnWave(_rng.Next(MinInitialItems, MaxInitialItems));

        //Run until the game ends
        while (this.GetGameManager().GetComponent<Level>().GameRunning)
        {
            //Wait for random seconds
            yield return new WaitForSeconds(RandomFloat(MinSpawnFrequency, MaxSpawnFrequency));

            //Spawn the next wave
            GenerateAndSpawnWave(_rng.Next(MinItemsPerWave, MaxItemsPerWave));
        }
    }

    /// <summary>
    /// Genrates a wave of items
    /// </summary>
    /// <param name="waveSize"></param>
    public void GenerateAndSpawnWave(int waveSize)
    {
        for (int i = 0; i < waveSize; i++)
        {
            GenerateAndSpawnItem();
        }
    }

    /// <summary>
    /// Generates a single junk item
    /// </summary>
    public void GenerateAndSpawnItem()
    {
        Instantiate(
            original: GetRandomItem(), 
            position: GetRandomPosition(), 
            rotation: Quaternion.Euler(0, 0, _rng.Next(360)
        ));
    }

    private Junk GetRandomItem()
    {
        if (SpawnInfo.Length == 0 || SpawnInfo.Max(i => i.SpawnChance) == 0)
        {
            throw new InvalidOperationException("No are set to spawn");
        }

        JunkSpawnRate candidate;

        do
        {
            candidate = SpawnInfo[_rng.Next(SpawnInfo.Length)];
        } while (_rng.NextDouble() > candidate.SpawnChance);

        return candidate.Item;
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 candidate;

        while (true)
        {
            //Generate random point
            candidate = new Vector2(
                RandomFloat(Bounds.min.x, Bounds.max.x),
                RandomFloat(Bounds.min.y, Bounds.max.y)
            );


            //If point is in exclusion zone, select a new point
            if (FindObjectsOfType<Collider2D>()
                .Where(i => i.gameObject.layer == LayerMask.NameToLayer(ExclustionZoneLayer)) //Colliders on the exclusion layer
                .Where(i => i.OverlapPoint(candidate)) //Colliders that overlap the point in question
                .Any()) //Are there any of those?
            {
                continue;
            }

            return candidate;
        }
    }

    /// <summary>
    /// Returns a random float. There is no easy way to do this with the .NET RNG
    /// </summary>
    /// <returns></returns>
    private float RandomFloat(float min, float max)
    {
        float candidate;

        do
        {
            candidate = _rng.Next((int)Math.Floor(min), (int)Math.Ceiling(max)) + (float)_rng.NextDouble();
        } while (candidate < min && candidate >= max);

        return candidate;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(Bounds.center, Bounds.size);
    }
}
