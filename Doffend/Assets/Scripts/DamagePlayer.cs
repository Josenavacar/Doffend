using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public GameObject player;
    private Health playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Health>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerScript.health -= 0.5f;

        if(playerScript.health <= 0)
        {
            playerScript.Die();
        }
    }
}
