using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCamera : MonoBehaviour
{
    private Transform player;
    private int xPosition, yPosition;
    public void Init(Transform player, int x, int y)
    {
        this.player = player;
        xPosition = x;
        yPosition = y;
    }
    private void LateUpdate()
    {
        //safety check, if the player is null
        if (player != null)
        {
            float movex = Mathf.Lerp(transform.position.x, player.position.x, 0.2f);
            float movey = Mathf.Lerp(transform.position.y, player.position.y, 0.2f);
            transform.position = new Vector3(movex, movey, transform.position.z);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -(xPosition - 6), xPosition - 8),
                Mathf.Clamp(transform.position.y, -(yPosition - 2), yPosition - 4), transform.position.z);
        }
        
    }
}
