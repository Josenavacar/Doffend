using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField]
    private float speed = 4f;



    void Start()
    {
        
    }

    IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyBulletAfterTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

}
