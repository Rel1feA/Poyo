
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TranferAreaType
{
    horizontal,
    vertical
}

public class TranferArea : MonoBehaviour
{
    public int leftOrDownRoomID;
    public int rightOrUpRoomID;
    public TranferAreaType type;
  
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch(type)
            {
                case TranferAreaType.horizontal:
                    if (collision.transform.position.x < transform.position.x)
                    {
                        TranferManager.Instance.TranferRoom(leftOrDownRoomID);
                    }
                    else if (collision.transform.position.x > transform.position.x)
                    {
                        TranferManager.Instance.TranferRoom(rightOrUpRoomID);
                    }
                    break;
                case TranferAreaType.vertical:
                    if (collision.transform.position.y < transform.position.y)
                    {
                        TranferManager.Instance.TranferRoom(leftOrDownRoomID);
                    }
                    else if (collision.transform.position.y > transform.position.y)
                    {
                        TranferManager.Instance.TranferRoom(rightOrUpRoomID);
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
