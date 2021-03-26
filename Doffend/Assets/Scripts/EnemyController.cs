using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;
    private float minDistance = 1f;
    private float range;

    public Rigidbody2D m_Rigidbody;

    // Update is called once per frame
    void Update()
    {
        range = Vector2.Distance(transform.position, target.position);

        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if(range < 0.4)
            {

                m_Rigidbody.AddForce(-target.position * 200 * Time.deltaTime);
            }
        }
        
        
    }
}
