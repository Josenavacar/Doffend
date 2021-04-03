using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnTime = 6f;
    private Vector2 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
       // InvokeRepeating("Spawn", spawnTime, spawnPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
       // spawnPosition.x = 
    }
}
