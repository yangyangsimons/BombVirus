using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombableWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ManageTags.bombAnimation))
        {
            Destroy(gameObject);
        }
    }
}
