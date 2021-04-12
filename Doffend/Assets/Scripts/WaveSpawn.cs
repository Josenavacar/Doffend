using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawn : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemy;

        public int amount;
        public float rate;
    }
    public enum SpawnState { SPAWNING, WAITING, COUNTING, FINISHED };
    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    public Transform leftSpawn;
    public Transform rightSpawn;
    private SpawnState state = SpawnState.COUNTING;
    public List<Wave> waves;
    private int nextWave = 0;
    private float timeElapsed = 0;
    private GameObject Score;

    public GameObject enemiesParent;
    private EnemyManager enemiesManager;

    //Wave Stuff
    public Wave wavePrime;
    public Wave waveStore;
    public GameObject goblinPrefab;
    private int waveCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        enemiesManager = enemiesParent.GetComponent<EnemyManager>();
        enemiesManager.OnGoblinDeath += GoblinDied;
        waveCountdown = timeBetweenWaves; 
        Score = GameObject.Find("Score");

        //WavePrime
        wavePrime.amount = 6;
        wavePrime.enemy = goblinPrefab;
        wavePrime.name = "Wave 1";
        wavePrime.rate = 1;
        waveCounter = 1;

        waves.Add(wavePrime);
        waveStore = wavePrime;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            timeElapsed += Time.deltaTime;
            if(!EnemyIsAlive())
            {
                //Finish current wave
                nextWave++;
                state = SpawnState.FINISHED;
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
            increaseWave(waveStore);
            if(waveCounter % 3 == 0)
            {
                buffGoblins();
            }
            state = SpawnState.COUNTING;
            waveCountdown = timeBetweenWaves;
        }

        if (waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                timeElapsed = 0;
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        } 
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    public void GoblinDied(object sender, EventArgs e)
    {
        Score.GetComponent<Score_Update>().score += 10;
        PlayerPrefs.SetInt("score", Score.GetComponent<Score_Update>().score);
    }

    bool EnemyIsAlive()
    {
        if(enemiesManager.enemiesLeft == 0)
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
        for(int i = 0; i < _wave.amount; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        System.Random random = new System.Random();
        int randomNum = random.Next(0, 10);
        Transform spawn;
        if(randomNum <= 5)
        {
            spawn = leftSpawn;
        }
        else
        {
            spawn = rightSpawn;
        }

        var newEnemy = Instantiate(_enemy, spawn.position, Quaternion.identity);
        newEnemy.transform.parent = enemiesParent.transform;
        Debug.Log("Spawning enemy: " + _enemy.name);
    }

    void increaseWave(Wave previousWave)
    {
        Wave newWave = new Wave();
        
        waveCounter++;
        if(previousWave.rate < 4)
        {
            newWave.amount = previousWave.amount + 2;
            newWave.enemy = previousWave.enemy;
            newWave.rate = previousWave.rate + 0.1f;
            newWave.name = "Wave " + waveCounter;
        }
        else
        {
            newWave.amount = previousWave.amount + 4;
            newWave.enemy = previousWave.enemy;
            newWave.rate = previousWave.rate;
            newWave.name = "Wave " + waveCounter;
        }

        waves.Add(newWave);
        waveStore = newWave;
    }

    void buffGoblins()
    {
        GameObject goblins = goblinPrefab;

        float damage = goblins.GetComponent<Enemy>().enemyDamage;
        float hp = goblins.GetComponent<Goblin_Health>().hitPoints;

        if(damage < 2.5)
        {
            goblins.GetComponent<Enemy>().enemyDamage = damage + .5f;
            goblins.GetComponent<Goblin_Health>().hitPoints = hp + 1;
        }
        else if(hp < 10)
        {
            goblins.GetComponent<Goblin_Health>().hitPoints = hp + 1;
        }
    }

    void resetGoblins()
    {
        GameObject goblins = goblinPrefab;

        goblins.GetComponent<Enemy>().enemyDamage = .5f;
        goblins.GetComponent<Goblin_Health>().hitPoints = 2f;
    }
}
