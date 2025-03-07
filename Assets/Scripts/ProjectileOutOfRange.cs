using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOutOfRange : MonoBehaviour
{
    // Maximum allowed range for the projectile before it is destroyed
    private static readonly float _range = 20.0f;

    // Update is called once per frame
    void Update()
    {
        // Check if the projectile has moved beyond the specified range in either the x or z direction
        if (Mathf.Abs(transform.position.x) > _range || Mathf.Abs(transform.position.z) > _range)
        {
            // Destroy the projectile if it is out of range
            Destroy(gameObject);
        }
    }
}

