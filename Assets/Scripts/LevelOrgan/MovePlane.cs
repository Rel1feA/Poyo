using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlane : MonoBehaviour
{
    public List<IOperator> operators = new List<IOperator>();
    public Vector3 offset = Vector3.zero;
    public Vector3 originPos;

    private void OnTriggerStay2D(Collider2D collision)
    {
        offset = transform.position - originPos;
        foreach (IOperator op in operators)
        {
            op.GetOperatorObject().transform.position += offset;
        }
        originPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        originPos = transform.position;
        if (collision.TryGetComponent<IOperator>(out IOperator _operator))
        {
            operators.Add(_operator);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IOperator>(out IOperator _operator))
        {
            operators.Remove(_operator);
        }
    }
}
