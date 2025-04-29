using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombRadius;
    public float damage;
    public float maxLifeTime;
    private float timer = 0;
    private bool hasExplosion;
    private Animator animator;
    private Rigidbody2D rb2D;
    private Collider2D collider2d;
    private void Start()
    {
        animator= GetComponent<Animator>();
        rb2D=GetComponent<Rigidbody2D>();
        collider2d=GetComponent<Collider2D>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>=maxLifeTime)
        {
            Explosion();
        }
    }

    public void Explosion()
    {
        if (hasExplosion) return;
        animator.SetTrigger("Explosion");
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0;
        rb2D.isKinematic = true;
        collider2d.enabled = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bombRadius);
        HashSet<ITakeDamge> damages = new HashSet<ITakeDamge>();
        foreach(Collider2D collider in colliders)
        {
            if(collider.TryGetComponent<ITakeDamge>(out ITakeDamge damge))
            {
                damages.Add(damge);
            }
        }
        foreach(ITakeDamge takeDamge in damages)
        {
            takeDamge.TakeDamge(damage);
        }
        hasExplosion = true;
    }

    public void OnExplosinAni()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explosion();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bombRadius);
    }
}
