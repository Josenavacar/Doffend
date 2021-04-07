using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Health : MonoBehaviour
{
    public float hitPoints = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerProjectile")
        {
            hitPoints -= 1;

            Destroy(other.gameObject);
        }
    }
}
