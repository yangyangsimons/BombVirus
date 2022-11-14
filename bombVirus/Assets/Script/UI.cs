using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    public static UI Instance;
    public Text T_HP, T_LEVEL, T_TIME;
    public GameObject gameoverUI,winningUI;
    //public GameObject gameStartUI;
    public Animator levelAnimation;
    private void Awake()
    {
        Instance = this;
        Init();
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        SceneManager.LoadScene(1);
    //        Time.timeScale = 1;
    //    }
    //}
    private void Init()
    {
        gameoverUI.transform.Find("bt_again").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        });
        winningUI.transform.Find("bt_again").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        });

        //gameStartUI.transform.Find("bt_start").GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //    gameStartUI.SetActive(false);
        //    Time.timeScale = 1;
        //});

    }
    public void RefreshUI(int life, int level, int time)
    {
        T_HP.text = "HP: " + life.ToString();
        T_LEVEL.text = "LEVEL: " + level.ToString();
        T_TIME.text = "TIME: " + time.ToString();
    }

    public void GameOverUI()
    {
        gameoverUI.SetActive(true);
    }
    public void WinningUI()
    {
        winningUI.SetActive(true);
    }
    //public void GameStartUI()
    //{
    //    gameStartUI.SetActive(true);
    //}

    public void levelAlert()
    {
        levelAnimation.Play("level",0,0);
    }
}
    