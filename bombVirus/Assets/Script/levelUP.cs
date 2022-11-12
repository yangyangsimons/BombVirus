using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is for levelUP management;
public class levelUP : MonoBehaviour
{


    //change the levelUP sprite;
    public Sprite levelUPSprite, originSprite;

    private SpriteRenderer levelUPSpriteRender;

    private void Awake()
    {
        levelUPSpriteRender = GetComponent<SpriteRenderer>();
    }
    public void ResetLevelUP()
    {
        //set the sprite to it's original sprite and the isTrigger attribute as false;
        tag = "bombableWall";
        gameObject.layer = 8;
        levelUPSpriteRender.sprite = originSprite;
        GetComponent<Collider2D>().isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ManageTags.bombAnimation))
        {
            // if the bomb hits the door then the wall become levelUP door;
            tag = "Untagged";
            gameObject.layer = 0;
            levelUPSpriteRender.sprite = levelUPSprite;
            GetComponent<Collider2D>().isTrigger = true;

        }

        //if the player hits the door and the virus is cleaned ,then, levelUP;
        if (collision.CompareTag(ManageTags.Player))
        {
            Destroy(gameObject);
            //determine whether the virus is cleaned;
            print("next level");
            GameController.Instance.LevelControl();
        }    
    }
}
