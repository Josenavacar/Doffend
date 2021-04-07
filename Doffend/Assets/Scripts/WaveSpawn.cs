using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemy;
        public int count;
        public float rate;
    }
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    public Transform spawn;

    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;

    public Wave[] waves;
    private int nextWave = 0;
    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = timeBetweenWaves; 
    }

    // Update is called once per frame
    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                state = SpawnState.COUNTING;
                nextWave++;
                //Finish current wave
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        } 
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectWithTag("Goblin") == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        Instantiate(_enemy, spawn.position, Quaternion.identity);
        Debug.Log("Spawning enemy: " + _enemy.name);
    }
}
