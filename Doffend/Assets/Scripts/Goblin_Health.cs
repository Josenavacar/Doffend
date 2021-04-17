using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Health : MonoBehaviour
{
    public float hitPoints = 2;
    private WaveSpawn script;
    private GameObject score;
    public GameObject papa;
    public Animator animator;

    private EnemyAI _EnemyAI;

    // Start is called before the first frame update
    void Start()
    {
        _EnemyAI = GetComponent<EnemyAI>();

        GameObject wavemanagement = GameObject.Find("WaveManager");
        papa = this.transform.parent.gameObject;
        script = wavemanagement.GetComponent<WaveSpawn>();

        score = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        if(hitPoints <= 0)
        {
            StartCoroutine(DeathState());
        }
    }
   

    private IEnumerator DeathState()
    {
        _EnemyAI.enabled = false;
        papa.GetComponent<EnemyManager>().GoblinDeath();
        animator.SetBool("Death", true);

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        BulletController script = other.GetComponent<BulletController>();
        if(script != null)
        {
            hitPoints -= script.damage;
        }
    }
}
