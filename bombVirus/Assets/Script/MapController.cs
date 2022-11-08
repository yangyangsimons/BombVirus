using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public GameObject solidWallPre, bombableWallPre, levelUPPre, propertiesPre, virusPre;
    // x = solid wall number in x axis;
    // y = solid wall number in y axis colloumns;
    private int xNumber, yNumber;
    private List<Vector3> emptyPointList = new List<Vector3>();
    private List<Vector3> solidWallPointList = new List<Vector3>();
    private GameObject levelUPDoor;
    //// provide 5 list to store object that get from the manageObject;
    //private List<GameObject> solidWallObjectList = new List<GameObject>();
    //private List<GameObject> bombableWallObjectList = new List<GameObject>();
    //private List<GameObject> propertiesObjectList = new List<GameObject>();
    //private List<GameObject> virusObjectList = new List<GameObject>();
    //private List<GameObject> ObjectList = new List<GameObject>();


    //determine whether is a solidwall
    public bool IsSolidWall(Vector3 position)
    {
        if (solidWallPointList.Contains(position))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //private void Awake()
    //{
    //    MapInit(5, 3, 7);
    //}
    // return the topleft corner location for generate the player;
    public Vector3 GetPlayerPostition()
    {
        return new Vector3(-(xNumber + 1), yNumber - 1);
    }
    public void MapInit(int x, int y, int bombaleNumber, int virusCount)
    {
        
        //reset the map and make the level UP function posible;
        emptyPointList.Clear();
        solidWallPointList.Clear();

        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }

        xNumber = x;
        yNumber = y;
        CreateSolidWall();
        AllocateEmptyPoints();
        //Debug.Log(emptyPointList.Count);
        CreateProperties();
        CreateBombableWall(bombaleNumber);
        CreateLevelUp();

        CreateVirus(virusCount);
    }

    private void CreateSolidWall()
    {
        // create inner solid wall;
        for (int i = -xNumber; i < xNumber; i += 2)
        {
            for (int j = -yNumber; j < yNumber; j += 2)
            {
                Vector3 solidWallPosition = new Vector3(i, j);
                CreateWalls(solidWallPre, solidWallPosition);
                solidWallPointList.Add(solidWallPosition);
                //GameObject innerSolidWall = Instantiate(solidWallPre, transform);
                //innerSolidWall.transform.position = new Vector3(i, j);
            }
        }

        //create outer solid wall;
        for (int i = -(xNumber + 2); i <= xNumber; i++)
        {
            Vector3 solidPosition1 = new Vector3(i, yNumber);
            Vector3 solidPosition2 = new Vector3(i, -yNumber - 2);
            CreateWalls(solidWallPre, solidPosition1);
            solidWallPointList.Add(solidPosition1);
            CreateWalls(solidWallPre, solidPosition2);
            solidWallPointList.Add(solidPosition2);
            //GameObject solidWallTopX = Instantiate(solidWallPre, transform);
            //solidWallTopX.transform.position = new Vector3(i, yNumber);
            //GameObject solidWallBottomX = Instantiate(solidWallPre, transform);
            //solidWallBottomX.transform.position = new Vector3(i, -yNumber - 2);
        }
        for (int j = -(yNumber + 1); j <= yNumber - 1; j++)
        {
            Vector3 solidPosition3 = new Vector3(-(xNumber + 2), j);
            Vector3 solidPosition4 = new Vector3(xNumber, j);
            CreateWalls(solidWallPre, solidPosition3);
            solidWallPointList.Add(solidPosition3);
            CreateWalls(solidWallPre,solidPosition4);
            solidWallPointList.Add(solidPosition4);
            //GameObject solidWallLeftY = Instantiate(solidWallPre, transform);
            //solidWallLeftY.transform.position = new Vector3(-(xNumber + 2), j);
            //GameObject solidWallRightY = Instantiate(solidWallPre, transform);
            //solidWallRightY.transform.position = new Vector3(xNumber, j);
        }

    }

    //create bombable walls;
    //locate all the empty point inside of the outer solid wall.
    private void AllocateEmptyPoints()
    {
        //locate all the empty location.
        for (int i = -(xNumber + 1); i < xNumber; i++)
        {
            if (-(xNumber + 1) % 2 == i % 2)
            {
                for (int j = -(yNumber + 1); j < yNumber; j++)
                {
                    emptyPointList.Add(new Vector3(i, j));
                }
            }
            else
            {
                for (int k = -(yNumber + 1); k < yNumber; k += 2)
                {
                    emptyPointList.Add(new Vector3(i, k));
                }
            }
        }
        //remove the top left 3 points for generating character space;
        emptyPointList.Remove(new Vector3(-(xNumber + 1), yNumber - 1));
        emptyPointList.Remove(new Vector3(-(xNumber + 1),yNumber -2 ));
        emptyPointList.Remove(new Vector3(-xNumber,yNumber-1));
    }


    //create properties in the bombable walls
    private void CreateProperties()
    {
        int propertiesNumber = (int)(solidWallPointList.Count * 0.06);
        print(propertiesNumber);
        for (int i = 0; i < propertiesNumber; i++)
        {
            //randomly select a empty location to generate the properties;
            int propertiesLocation = Random.Range(0, emptyPointList.Count);
            CreateWalls(propertiesPre, emptyPointList[propertiesLocation]);
            emptyPointList.RemoveAt(propertiesLocation);
        }
    }

    //create the level Up door;
    private void CreateLevelUp()
    {
        print("crete level up");
        int levelUPLocation = Random.Range(0, emptyPointList.Count);
        levelUPDoor = Instantiate(levelUPPre, transform);
        levelUPDoor.transform.position = emptyPointList[levelUPLocation];
        levelUPDoor.GetComponent<levelUP>().ResetLevelUP();
        //levelUPDoor.transform.position = emptyPointList[levelUPLocation];
        emptyPointList.RemoveAt(levelUPLocation);
        print("crete level up end");
    }
    //create the bombable wall in the empty point.
    private void CreateBombableWall(int bombableWallNUmber)
    {
        ////testing system, create bombable wall in all the empty points.
        //for (int i = 0; i < emptyPointList.Count; i++)
        //{
        //    CreateWalls(bombableWallPre ,emptyPointList[i]);
        //}

        //in case that bomablewallnumber is greater than empty point
        //then the bomable wall number is 60% of the empty point;
        if (bombableWallNUmber >= emptyPointList.Count)
        {
            bombableWallNUmber =(int)(emptyPointList.Count * 0.6);
        }
        for (int i = 0; i < bombableWallNUmber; i++)
        {
            //use a ramdom number to create the bombable wall in empty ponit;
            int bombableWallLOcation = Random.Range(0, emptyPointList.Count);
            CreateWalls(bombableWallPre, emptyPointList[bombableWallLOcation]);
            emptyPointList.RemoveAt(bombableWallLOcation);
        }
    }



    // create virus
    private void CreateVirus(int virusNumber)
    {
        for (int i = 0; i < virusNumber; i++)
        {
            int virusLocation = Random.Range(0, emptyPointList.Count);
            CreateWalls(virusPre, emptyPointList[virusLocation]);
            //GameObject virusObject = Instantiate(virusPre, transform);
            //virusObject.GetComponent<VirusAI>().Init();
            //print(emptyPointList[virusLocation]);
            emptyPointList.RemoveAt(virusLocation);
        }
    }
    private void CreateWalls(GameObject tyepOfWall, Vector3 pos)
    {
        //this is for create the wall attribute elements, not only restricted to be walls;
        //this function also used to create virus and properties;
        GameObject pointWall = Instantiate(tyepOfWall, transform);
        pointWall.transform.position = pos;
    }


}