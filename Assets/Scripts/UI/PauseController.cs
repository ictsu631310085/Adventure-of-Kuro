using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject settingScreen;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    private SettingManager settingManager;

    public bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        settingManager = settingScreen.GetComponent<SettingManager>();
        settingManager.Initialize();

        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && Input.GetButtonDown("Cancel"))
        {
            switch (pauseScreen.activeInHierarchy)
            {
                case false:
                    pauseScreen.SetActive(true);
                    Time.timeScale = 0;
                    break;
                case true:
                    settingScreen.SetActive(false);
                    ResumeButton();
                    break;
            }
        }

        if (isGameOver)
        {
            Time.timeScale = 0;
        }
        else if (!pauseScreen.activeInHierarchy)
        {
            Time.timeScale = 1;
        }

        if (!GameObject.FindGameObjectWithTag("Level_1_Boss"))
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
        }
    }

    public void ResumeButton()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_1");
    }

    public void SettingButton()
    {
        settingScreen.SetActive(true);
    }

    public void TitleButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackButton(GameObject target)
    {
        target.SetActive(false);
    }
}
