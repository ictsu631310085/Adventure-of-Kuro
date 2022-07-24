using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP_HUD : MonoBehaviour
{
    public PlayerData playerData;

    public Image HP_1;
    public Image HP_2;
    public Image HP_3;

    public PauseController pauseController;
    public GameObject gameOverScreen;

    // Update is called once per frame
    void Update()
    {
        switch (playerData.currentHP)
        {
            case 3:
                HP_1.color = Color.white;
                HP_2.color = Color.white;
                HP_3.color = Color.white;
                break;
            case 2:
                HP_1.color = Color.white;
                HP_2.color = Color.white;
                HP_3.color = Color.black;
                break;
            case 1:
                HP_1.color = Color.white;
                HP_2.color = Color.black;
                HP_3.color = Color.black;
                break;
            case 0:
                HP_1.color = Color.black;
                HP_2.color = Color.black;
                HP_3.color = Color.black;
                GameOver();
                break;
        }
    }

    public void GameOver()
    {
        pauseController.isGameOver = true;
        gameOverScreen.SetActive(true);
    }
}
