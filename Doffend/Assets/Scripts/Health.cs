using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 5;
    public int numOfHearts = 5;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    // Update is called once per frame
    void Update()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }
        
        healthCheck();
    }

    public void healthCheck()
    {
        bool half = false;
        int auxHealth = (int)health;
        
        if(health*10 % 10 != 0)
        {
            half = true;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < auxHealth)
            {
                hearts[i].sprite = fullHeart;   
            } 
            else if(i >= auxHealth && half)
            {
                hearts[i].sprite = halfHeart;
                half = false;
            }
            else {
                hearts[i].sprite = emptyHeart;
            }


            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            } else 
            {
                hearts[i].enabled = false;
            }
        }
    }
}
