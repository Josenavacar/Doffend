using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    //The max lives that a player can have, let us take 5
    int maxLives = 5;

    //Each life replenishes in 15minutes or 900 seconds
    float lifeReplenishTime = 900f;

    // The number of lives that the player has
    int _lives;
    public int lives{
        set {
            _lives = value; 
            PlayerPrefs.SetInt("Lives", _lives);
        }
        get {
            return _lives;
        }
    }
}
