using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad;
    public GameObject settingsWindow;

    public void CloseButton()
    {
        Application.Quit();
    }

    public void LevelsButton()
    {
    }

    public void StartGame()
    {
        SceneManager.LoadScene(LevelToLoad);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OpenSettingsWindowButton()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindowButton()
    {
        settingsWindow.SetActive(false);
    }
}
