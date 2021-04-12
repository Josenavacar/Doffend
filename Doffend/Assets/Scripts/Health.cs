using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Health : MonoBehaviour
{
    public float health = 5;
    public int numOfHearts = 5;
    public GameObject player;
    public GameObject canvas;
    private Health_UI UIScript;

    void Start()
    {
        UIScript = canvas.GetComponent<Health_UI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Enemy>() != null)
        {
            float damage = other.gameObject.GetComponent<Enemy>().enemyDamage;
            health -= damage;
            UIScript.updateUI();

            if(health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        UIScript.deathUI();
        SceneManager.LoadScene("Menu");
    }
}
