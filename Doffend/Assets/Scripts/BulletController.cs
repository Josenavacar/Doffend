using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField]
    private float speed = 4f;

    public float damage = .5f;

    public float timeUntilDisappear = 3f;

    IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(timeUntilDisappear);
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

   

}
