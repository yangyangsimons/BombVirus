using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAI : MonoBehaviour
{
    //set the virus rigid body
    private Rigidbody2D rigidVirus;

    //set the virus move direction 0 - up, 1 - down, 2 - left, 3 - right;
    //set the default direction as 0;
    private int directionIndex = 0;

    //set the virus move speed;
    private float virusSpeed = 0.025f;

    //set the raycast range
    private float rayRange = 0.5f;
    private Vector2 virusMove;

    //determine if the virus is srrounded by walls;
    //private bool notSrrounded = true;

    private void Awake()
    {
        rigidVirus = GetComponent<Rigidbody2D>();
        InitDirection(Random.Range(0, 4));

    }
    public void Init()
    {
        //randomly select a virus direction by random range 0 - 3;
        InitDirection(Random.Range(0, 4));
    }
    private void InitDirection(int index)
    {
        directionIndex = index;
        switch (directionIndex)
        {
            case 0:
                virusMove = Vector2.up;
                break;
            case 1:
                virusMove = Vector2.down;
                break;
            case 2:
                virusMove = Vector2.left;
                break;
            case 3:
                virusMove = Vector2.right;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        //because the rigidVirus is 2d,so the moveposition need a vector2 parameter;
        //so compulsory transfer vector3 to vector2;

        //print("moving");
        rigidVirus.MovePosition(((Vector2)transform.position) + (virusMove * virusSpeed));

    }


    //determine if the virus hits a wall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //only the virus hits the solid wall and bombable wall will tigger the affect;
        if (collision.CompareTag(ManageTags.solidWall) || collision.CompareTag(ManageTags.bombableWall))
        {
            //reset the virus position;
            transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            hitWallChangeDirection();
        }
        //if the bombAnimation hit the virus then virus destory themselves;
        if (collision.CompareTag(ManageTags.bombAnimation))
        {
            Destroy(gameObject);
        }

    }
    //use the line to mock the raycast

    private void hitWallChangeDirection()
    {
        //reset the virus position;
        //transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        ////creat a List to store the collision status and the direction value;
        List<int> virusDirectionList = new List<int>();
        if (Physics2D.Raycast(transform.position, Vector2.up, rayRange, 1 << 8) == false)
        {
            virusDirectionList.Add(0);
        }
        if (Physics2D.Raycast(transform.position, Vector2.down, rayRange, 1 << 8) == false)
        {
            virusDirectionList.Add(1);
        }
        if (Physics2D.Raycast(transform.position, Vector2.left, rayRange, 1 << 8) == false)
        {
            virusDirectionList.Add(2);
        }
        if (Physics2D.Raycast(transform.position, Vector2.right, rayRange, 1 << 8) == false)
        {
            virusDirectionList.Add(3);
        }
        //print(virusDirectionList.Count);
        //print(virusDirectionList.Count);
        if (virusDirectionList.Count > 0)
        {
            //notSrrounded = true;
            //if the virus do hits a wall then randomly select a direction that do not detected the wall from the list;
            int directionIndex = Random.Range(0, virusDirectionList.Count);
            InitDirection(virusDirectionList[directionIndex]);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, rayRange, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -rayRange, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-rayRange, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(rayRange, 0, 0));
    }
}
