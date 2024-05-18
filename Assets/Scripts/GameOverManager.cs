using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public GameObject GameoverUI;

    public static GameOverManager instance;
    private Animator fadeSystem;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }

        instance = this;

        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    public void OnPlayerDeath()
    {
        GameoverUI.SetActive(true);
    }

    //button name : Restart
    public void RestartButton()
    {
        StartCoroutine(RestartButtonCoroutine());
    }

    public IEnumerator RestartButtonCoroutine()
    {
        //enlever le menu
        GameoverUI.SetActive(false);

        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.5f);

        //recharger la scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        //Respawn le joueur + vie
        PlayerHealth.instance.Respawn();
        
        //enlever la progression du niveau actuel
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedUpCountInThisScence);

    }

    //button name : Back
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //button name : Levels
    public void LevelsButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    //button name : Leaderboard
    public void LeaderBoardButton()
    {
        
    }

    //button name : Close
    public void QuitButton()
    {
        Application.Quit();
    }
}
