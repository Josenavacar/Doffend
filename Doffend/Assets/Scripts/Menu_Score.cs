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

        if(PlayerPrefs.GetInt("score") != null)
        {
            last_score = PlayerPrefs.GetInt("score");
        }

        last.GetComponent<TMP_Text>().text = "Last Score: " + last_score;
    }
}
