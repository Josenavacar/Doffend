using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu_Score : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Find game objects
        GameObject last = GameObject.Find("LastScore");
        GameObject hi = GameObject.Find("HiScore");

        //Set last_score
        int last_score = PlayerPrefs.GetInt("score", 0);
        last.GetComponent<TMP_Text>().text = "Last Score: " + last_score;

        //Find if we had a previous hiScore and compare it with the last score
        int hiScore = PlayerPrefs.GetInt("hiscore", 0);
        if(hiScore < PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("hiscore", PlayerPrefs.GetInt("score"));
        }

        //Set hi score
        hi.GetComponent<TMP_Text>().text = "Hi-Score: " + PlayerPrefs.GetInt("hiscore");
    }
}
