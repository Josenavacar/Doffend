using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveNumber : MonoBehaviour
{
    public int waveNumber = 0;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TMP_Text>().text = "Wave " + waveNumber.ToString();
    }
}
