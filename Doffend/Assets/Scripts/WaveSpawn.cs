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
    private float timeElapsed = 0;
    private GameObject Score;
    
    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = timeBetweenWaves; 
        Score = GameObject.Find("Score");

    }

    // Update is called once per frame
    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            timeElapsed += Time.deltaTime;
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
            int timeToScore = (int) Mathf.Max(0, 50 - timeElapsed) * 5;
            Score.GetComponent<Score_Update>().score += timeToScore;
            PlayerPrefs.SetInt("score", Score.GetComponent<Score_Update>().score);
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
