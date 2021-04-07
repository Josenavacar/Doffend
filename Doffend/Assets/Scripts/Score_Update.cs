using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Update : MonoBehaviour
{
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TMP_Text>().text = score.ToString();
    }
}
