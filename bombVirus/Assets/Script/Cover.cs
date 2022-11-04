using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cover : MonoBehaviour
{
    public GameObject btn_start;
    private void Awake()
    {
        btn_start.transform.Find("btn_start").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);

        });
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        SceneManager.LoadScene(1);

    //    }
    //}


}
