using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bomb : MonoBehaviour
{
    //set the bomb default range;
    private int range;
    //private Action aniFinAction;

    // create a bomb animation for bombing;
    public GameObject bombAnimation;

    //store the finnish action
    private Action animationFinish;
    public void Init(int range, float dealyTime, Action action)
    {
        //store the range for later use;
        this.range = range;
        StartCoroutine("LaterBombing", dealyTime);
        animationFinish = action;
    }
    IEnumerator LaterBombing(float time)
    {
        yield return new WaitForSeconds(time);
        if (animationFinish != null) animationFinish();
        Instantiate(bombAnimation, transform.position, Quaternion.identity);

        ManageAudio.Instance.Bombingbg();
        //generate the animation in the four directions;
        BombRange(Vector2.left);
        BombRange(Vector2.right);
        BombRange(Vector2.down);
        BombRange(Vector2.up);

        Destroy(gameObject);
        //AudioController.Instance.PlayBoom();

        //ObjectPool.Instance.Get(ObjectType.bombAnimation, transform.position);


        //ObjectPool.Instance.Add(ObjectType.Bomb, gameObject);
    }
    private void BombRange(Vector2 direction)
    {
        for (int i = 1; i <= range; i++)
        {
            Vector2 location = (Vector2)transform.position + direction * i;
            if (GameController.Instance.IsSolidWall(location)) break;
            GameObject bombing = Instantiate(bombAnimation);
            bombing.transform.position = location;
            //ObjectPool.Instance.Get(ObjectType.BombEffect, pos);
        }
    }
}
