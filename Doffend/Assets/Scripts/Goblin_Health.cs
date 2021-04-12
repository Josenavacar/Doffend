using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Health : MonoBehaviour
{
    public float hitPoints = 2;
    private WaveSpawn script;
    private GameObject score;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject wavemanagement = GameObject.Find("WaveManager");
        script = wavemanagement.GetComponent<WaveSpawn>();

        score = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        if(hitPoints <= 0)
        {
            script.enemiesLeft--;
            score.GetComponent<Score_Update>().score += 10;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BulletController script = other.GetComponent<BulletController>();
        if(script != null)
        {
            hitPoints -= script.damage;

            Destroy(other.gameObject);
        }
    }
}
