using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum defining different power-up types
public enum PowerType
{
    Superpower,   // Grants the player increased strength
    Projectiles,  // Allows the player to shoot projectiles
    Smash         // Enables the player to perform a smash attack
};

// Class representing a power-up object
public class Powerup : MonoBehaviour
{
    // The type of power-up this object represents
    public PowerType PowerType;
}

