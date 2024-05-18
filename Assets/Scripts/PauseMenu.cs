using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    public GameObject settingsWindow;
    
    public void pauseMenuButton()
    {
        if (GameIsPaused)
        {
            Resume();
        } else {
            Paused();
        }
    }

    public void Resume()
    {        
        //remettre les mouvements pendant le menu
        PlayerMovement.instance.enabled = true;

        // Afficher le menu pause
        PauseMenuUI.SetActive(false);
        
        // Arreter le temps
        Time.timeScale = 1;

        // Changer le status du jeu
        GameIsPaused = false;
    }

    private void Paused()
    {
        //eviter les mouvements pendant le menu
        PlayerMovement.instance.enabled = false;

        // Afficher le menu pause
        PauseMenuUI.SetActive(true);
        
        // Arreter le temps
        Time.timeScale = 0;

        // Changer le status du jeu
        GameIsPaused = true;
    }

    public void OpenSettingsWindowButton()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindowButton()
    {
        settingsWindow.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelSelect()
    {        
        Resume();
        SceneManager.LoadScene("LevelSelect");
    }
}
