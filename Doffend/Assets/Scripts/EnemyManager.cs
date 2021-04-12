using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemiesLeft;

    public event EventHandler OnGoblinDeath;

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
