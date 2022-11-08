using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // set an Animator Object for animation creation
    // set a rigidbody object for player collider;
    private Animator anima;
    private Rigidbody2D rigidPlayer;
    private SpriteRenderer spriteRenderer;
    //set the player move speed;
    public float moveSpeed = 0.045f;

    //set player HP --life value
    public int HP = 1;
    private Color playerColor;
    public int bombNumber = 1;
    //determine if the player is immune;
    private bool IsImmune = false;

    // get the bomb pre;
    public GameObject bombPre;

    //set the default bombing time and bombing range;
    private float bombingTime = 1.5f;
    public int bombingRange = 1;
    private List<GameObject> bombList = new List<GameObject>();

    private void Awake()
    {
        anima = GetComponent<Animator>();
        rigidPlayer = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerColor = spriteRenderer.color;
    }

    public void Init(int bombingRange, int HP, float bombTime)
    {
        this.bombingRange = bombingRange;
        this.HP = HP;
        this.bombingTime = bombTime;
    }

    private void Update()
    {
        
        //float speed1 = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        //transform.Rotate(0, speed1, 0);
        //float speed2 = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        //transform.Rotate(0, speed2, 0);


        //set the horizontal move and vertical move condition;
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        //print("h:" + horizontalMove + "v: " + verticalMove);
        //change the value of horizontal and vertical as move trigger;
        anima.SetFloat("Horizontal", horizontalMove);
        anima.SetFloat("Vertical", verticalMove);
        //
        rigidPlayer.MovePosition(transform.position + new Vector3(horizontalMove, verticalMove) * moveSpeed);

        // create bomb;
        CreateBomb();

    }


    //player hurt function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsImmune) return;
        if (collision.CompareTag("virus")||collision.CompareTag("bombAnimation"))
        {

            HP--;
            StartCoroutine("PlayerHurt", 2f);
            if (HP <= 0)
            {
                anima.SetTrigger("Death");
            }
        }
    }
    IEnumerator PlayerHurt(float time)
    {
        //when the player get hurted, then immune for 2 seconds;
        IsImmune = true;
        for (int i = 0; i < time*2; i++)
        {
            playerColor.a = 0;
            spriteRenderer.color = playerColor;
            yield return new WaitForSeconds(0.25f);
            playerColor.a = 1;
            spriteRenderer.color = playerColor;
            yield return new WaitForSeconds(0.25f);
        }
        //immune timeout
        IsImmune = false;
    }

    //death animation
    public void DeathAnimation()
    {
        anima.SetTrigger("Death");
    }
    private void DeathAnimationFinish()
    {
        UI.Instance.GameOverUI();
        Time.timeScale = 0;
    }
    //create bomb
    private void CreateBomb()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)&& bombNumber>0)
        {

            bombNumber--;
            ManageAudio.Instance.SetBoombg();
            GameObject bomb = Instantiate(bombPre);
            bomb.transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),Mathf.RoundToInt(transform.position.y));
            bomb.GetComponent<Bomb>().Init(bombingRange, bombingTime, animationFinish);
        }

    }
    private void animationFinish()
    {
        //after the bombing animation finished, bomb number + 1;
        bombNumber++;
        //print("finish");
    }
}
