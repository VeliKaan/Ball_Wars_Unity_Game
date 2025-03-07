using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition
{
    // The range within which enemies or objects can spawn
    private static readonly float _spawnRange = 9;

    // Static method to generate a random spawn position within the specified range
    public static Vector3 Generate()
    {
        // Generate random X and Z coordinates within the spawn range
        float spawnPosX = Random.Range(-_spawnRange, _spawnRange);
        float spawnPosZ = Random.Range(-_spawnRange, _spawnRange);

        // Return the generated position with Y coordinate set to 0 (for a flat spawn on the ground)
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }
}
