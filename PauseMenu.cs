using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PasueMenuUI;

    private void Start()
    {
        PasueMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Can't use p for pause in simon says
        if (Input.GetKeyDown(KeyCode.P) && !StaticVar.inSimonSays)
        {
            
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }

        }
        
    }


    public void Resume()
    {
        Debug.Log("Resuming Game");

        PasueMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    public void Pause()
    {
        Debug.Log("Pausing Game");

        PasueMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
