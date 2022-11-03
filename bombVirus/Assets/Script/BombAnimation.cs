using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAnimation : MonoBehaviour
{
    private void FinishBomb()
    {
        Destroy(gameObject);
    }
}
