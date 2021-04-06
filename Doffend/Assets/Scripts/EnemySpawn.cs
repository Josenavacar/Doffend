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
      InvokeRepeating("Spawn", spawnTime, spawnTime);
   }

   void Spawn()
   {
      spawnPosition.x = 0.141f;
      spawnPosition.y = -8.176f;

      Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length -1)], spawnPosition, Quaternion.identity);
   }
   
}