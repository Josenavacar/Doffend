using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_UI : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public GameObject player;
    private Health playerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Health>();
    }

    public void updateUI()
    {
        bool half = false;
        int auxHealth = (int)playerScript.health;
        if(playerScript.health*10 % 10 != 0)
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
        }
    }

    public void deathUI()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = emptyHeart;
        }
    }
}
