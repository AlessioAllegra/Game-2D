using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.layer == 8)
        {
            GameManager.Instance.TriggerOnPlayerWin();
        }
    }
}
