using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;



public class Health : MonoBehaviour
{
    public float health = 5;
    public int numOfHearts = 5;
    public GameObject player;
    public GameObject canvas;
    private Health_UI UIScript;

    public event EventHandler OnPlayerDeath;

    private AudioSource _audioSrc;

    void Start()
    {
        UIScript = canvas.GetComponent<Health_UI>();
        _audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }
    }

    private void PlayDamagedSound()
    {
        _audioSrc.pitch = Random.Range(1.5f, 1.6f);
        //play oneshot
        _audioSrc.PlayOneShot(_audioSrc.clip);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Enemy>() != null)
        {
            float damage = other.gameObject.GetComponent<Enemy>().enemyDamage;
            health -= damage;
            PlayDamagedSound();
            UIScript.updateUI();

            if(health <= 0)
            {
                PlayerDeath();
            }
        }
    }

    public void PlayerDeath()
    {
        UIScript.deathUI();
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        SceneManager.LoadScene("Menu");
    }
}
