using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombRadius;
    public float damage;
    public float maxLifeTime;
    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>=maxLifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void Explosion()
    {
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explosion();
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bombRadius);
    }
}
