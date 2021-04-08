using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu_Score : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        int last_score = 0;
        GameObject last = GameObject.Find("LastScore");
        GameObject hi = GameObject.Find("HiScore");

        if(PlayerPrefs.GetInt("score") != null)
        {
            last_score = PlayerPrefs.GetInt("score");
        }

        last.GetComponent<TMP_Text>().text = "Last Score: " + last_score;


        if(PlayerPrefs.GetInt("hiscore") == null)
        {
            PlayerPrefs.SetInt("hiscore", PlayerPrefs.GetInt("score"));
        }
        else
        {
            if(PlayerPrefs.GetInt("hiscore") < PlayerPrefs.GetInt("score"))
            {
                PlayerPrefs.SetInt("hiscore", PlayerPrefs.GetInt("score"));
            }
        }
        hi.GetComponent<TMP_Text>().text = "Hi-Score: " + PlayerPrefs.GetInt("hiscore");
    }
}
