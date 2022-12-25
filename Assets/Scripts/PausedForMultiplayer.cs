using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedForMultiplayer : MonoBehaviour
{
    [SerializeField]
    GameObject pause;

    

    void Start()
    {
        pause.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {           
            pause.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(false);
        }
    }

    public void PauseOFF()
    {
        pause.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }


}