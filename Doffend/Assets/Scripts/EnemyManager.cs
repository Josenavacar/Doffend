using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemiesLeft;

    public event EventHandler OnGoblinDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemiesLeft = this.transform.childCount;
    }

    public void GoblinDeath()
    {
        OnGoblinDeath?.Invoke(this, EventArgs.Empty);
    }
}
