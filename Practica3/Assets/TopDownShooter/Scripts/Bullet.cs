using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float rotationSpeed;

   

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy (gameObject);
    }
    void Update()
    {
        var dir = Random.Range(-1, 1);
        transform.Rotate(0, 0, dir * rotationSpeed);
    }
}
