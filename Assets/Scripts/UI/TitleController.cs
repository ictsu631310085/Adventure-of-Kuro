using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    private SettingManager settingManager;
    private AudioSource audioSource;

    public GameObject settingScreen;
    public GameObject creditScreen;

    void Awake()
    {
        settingManager = settingScreen.GetComponent<SettingManager>();
        audioSource = GetComponent<AudioSource>();
        settingManager.Initialize();
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void SettingButton()
    {   
        settingScreen.SetActive(true);
    }

    public void CreditButton()
    {
        creditScreen.SetActive(true);
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
