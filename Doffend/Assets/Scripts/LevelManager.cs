using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject Player;
    public Text lifeCounter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillPlayer()
    {
        Debug.Log("Player Ded");
        Destroy(Player);
    }

    public void DamagePlayer()
    {
        int currentHP = int.Parse(lifeCounter.text.ToString());

        if(currentHP == 1)
        {
            KillPlayer();
        }
        else
        {
            int HP_after_damage = currentHP - 1;
            lifeCounter.text = HP_after_damage.ToString();
        }
    }
}
