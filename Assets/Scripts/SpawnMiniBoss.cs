using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMiniBoss : MonoBehaviour
{
    // Prefab of the mini-boss to spawn
    public GameObject MiniBoss;

    // Time interval between mini-boss spawns
    public float SpawnPeriod = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Invoke the Spawn method repeatedly with the specified SpawnPeriod
        InvokeRepeating(nameof(Spawn), SpawnPeriod, SpawnPeriod);
    }

    // Method to spawn the mini-boss at a random position
    private void Spawn()
    {
        // Generate a random spawn position
        Vector3 spawnPos = SpawnPosition.Generate();

        // Instantiate the mini-boss at the generated position with its original rotation
        Instantiate(MiniBoss, spawnPos, MiniBoss.transform.rotation);
    }
}
