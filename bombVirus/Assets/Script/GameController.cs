using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    //create the player prefab for player generation;
    public GameObject playerPre;
    //get the mapcontroller script;
    private MapController mapController;

    //set the default game level and the time count;
    private int levelNumber = 0;
    private int time = 160;
    private float timerCount = 0f;
    //store the player object before destory it.
    private GameObject player;
    private PlayerController playerInfo;
    private void Awake()
    {
        Instance = this;
    }

    //set the level control;
    public void LevelControl()
    {

        time = levelNumber * levelNumber * 60 + 180;
        int xNumber = 6 + (int)((levelNumber * levelNumber) / 2);
        int yNumber = 3 + (int)((levelNumber * levelNumber) / 3);
        int bombableWallNumber = 8  + levelNumber * levelNumber * 7;
        int virusNumber = 2 + 2 * levelNumber * levelNumber;
        mapController.MapInit(xNumber, yNumber, bombableWallNumber, virusNumber);

        //if the player is null
        if (player == null)
        {
            player = Instantiate(playerPre);
            playerInfo = player.GetComponent<PlayerController>();
            playerInfo.Init(1, 2, 1f);
        }

        player.transform.position = mapController.GetPlayerPostition();


        //get the main camera follow plaer
        //from the begining, there is no need for camera to follow player
        // when the graphic become large enough, let the camera follow player;
        if (levelNumber >= 2)
        {
            Camera.main.GetComponent<ManageCamera>().Init(player.transform,xNumber,yNumber);
        }
        //Camera.main.GetComponent<ManageCamera>().Init(player.transform);
        levelNumber++;
        
        if (levelNumber >= 7)
        {
            UI.Instance.WinningUI();
            Time.timeScale = 0;
        }
        UI.Instance.levelAlert();
        ManageAudio.Instance.Loading();

    }


    private void Start()
    {
        mapController = GetComponent<MapController>();
        //init the map;
        //mapController.MapInit(8, 3, 40, 30);
        //playerInfo.Init(1, 2, 1.5f);
        LevelControl();
        //Time.timeScale = 0;
        //UI.Instance.GameStartUI();
        //generat the player from the start position topleft corner;
        //GameObject player = Instantiate(playerPre);
        //player.transform.position = mapController.GetPlayerPostition();
    }

    private void TimeCount()
    {
        //if time is less or equal 0 , game over;
        if (time <= 0)
        {
            player.GetComponent<PlayerController>().DeathAnimation();
            //show the gameover UI
            UI.Instance.GameOverUI();
            Time.timeScale = 0;
        }
        else
        {
            timerCount += Time.deltaTime;
            if (timerCount >= 1.0f)
            {
                time--;
                timerCount = 0;
            }
        }

    }
    private void Update()
    {
        TimeCount();
        //Top bar UI refresh, include HP and level;
 
        if (Input.GetKeyDown(KeyCode.B))
        {
            LevelControl();
        }
        UI.Instance.RefreshUI(playerInfo.HP, levelNumber, time);
    }

    //encapsulation this function as the API for Bomb.cs calling;
    public bool IsSolidWall(Vector3 realPosition)
    {
        return mapController.IsSolidWall(realPosition);
    }
}
