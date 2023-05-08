using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public int life;
    public Rigidbody2D rb;
    public float knockBack;

    public Transform target;
    public float speed;
    [SerializeField] Animator anim;

    private void Start()
    {
        anim.SetBool("IsMoving", true);
    }
    private void Update()
    {
        Debug.Log(target);
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            life--;

            var dir = collision.transform.position - transform.position;
            rb.AddForce(-dir * knockBack, ForceMode2D.Force);
            CheckLife();
        }
    }

    void CheckLife()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
