using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlane : MonoBehaviour
{
    [HideInInspector]
    public List<Rigidbody2D> rb2ds = new List<Rigidbody2D>();
    public Vector2 offset = Vector2.zero;
    public Vector2 originPos;
    public Rigidbody2D rb2D;

    private void OnTriggerStay2D(Collider2D collision)
    {
        offset = rb2D.position - originPos;
        foreach (Rigidbody2D _rb2d in rb2ds)
        {
            if(CanMove(_rb2d,offset))
            {
                _rb2d.position += offset;
            }
        }
        originPos = rb2D.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        originPos = rb2D.position;
        if (collision.TryGetComponent<IOperator>(out IOperator _operator))
        {
            Rigidbody2D _rb2d=collision.GetComponent<Rigidbody2D>();
            if (_rb2d != null)
            {
                rb2ds.Add(_rb2d);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IOperator>(out IOperator _operator))
        {
            Rigidbody2D _rb2d = collision.GetComponent<Rigidbody2D>();
            if(_rb2d != null)
            {
                if (rb2ds.Contains(_rb2d))
                {
                    rb2ds.Remove(_rb2d);
                }
            }
        }
    }

    public bool CanMove(Rigidbody2D rb2D,Vector2 offset)
    {
        RaycastHit2D[] hit2D = new RaycastHit2D[10];
        ContactFilter2D contactFilter2D=new ContactFilter2D();
        contactFilter2D.SetLayerMask(LayerMask.GetMask("Obstacle","Ground"));
        float castDistance = 0.15f;
        if (rb2D.Cast(offset.normalized,contactFilter2D ,hit2D,offset.magnitude + castDistance)>1)
        {
            return false;
        }
        return true;
    }
}
