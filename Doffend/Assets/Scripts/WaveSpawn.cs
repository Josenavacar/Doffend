using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<GameObject> enemy;
        public float rate;
    }
    public enum SpawnState { SPAWNING, WAITING, COUNTING, FINISHED };
    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    public Transform spawn;

    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;

    public List<Wave> waves;
    public int enemiesLeft;
    private int nextWave = 0;


    bool done = false;
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
                if(waves[nextWave++] != null)
                {
                    nextWave++;
                }

                state = SpawnState.FINISHED;
                //Finish current wave
            }
            else
            {
                return;
            }
        }

        if(state == SpawnState.FINISHED)
        {
            Application.LoadLevel("Menu");
        }

        if (waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                enemiesLeft = waves[nextWave].enemy.Count;
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
        /*
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        return true;
        */

        if(enemiesLeft == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;
        for(int i = 0; i < _wave.enemy.Count; i++)
        {
            SpawnEnemy(_wave.enemy[i]);
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
