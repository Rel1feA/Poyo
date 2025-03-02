using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombRadius;
    public float explosionForce;
    private bool hasExplosion;

    public void Explosion()
    {
        if(hasExplosion)return;
       Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bombRadius);
       foreach(Collider2D collider in colliders)
       {
            if(collider.TryGetComponent<ITakeDamge>(out ITakeDamge damge))
            {
                damge.TakeDamge(() =>
                {
                    if (collider.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2D))
                    {
                        Vector2 forceDir=(rb2D.transform.position- transform.position).normalized;
                        rb2D.AddForce(forceDir*explosionForce,ForceMode2D.Impulse);
                    }
                });
            }
       }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explosion();
        hasExplosion = true;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bombRadius);
    }
}
