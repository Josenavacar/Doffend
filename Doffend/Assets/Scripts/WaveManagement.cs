using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagement : MonoBehaviour
{
    public GameObject goblin;
    public Transform leftSpawn;
    public Transform rightSpawn;

    public void SpawnGoblinLeft()
    {
        Instantiate(goblin, leftSpawn.position, Quaternion.identity);
    }

    public void SpawnGoblinRight()
    {
        Instantiate(goblin, rightSpawn.position, Quaternion.identity);
    }
}
