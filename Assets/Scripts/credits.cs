using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    public void loadMainMenu()
    {

        SceneManager.LoadScene("MainMenu");
    }

/*    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }*/
}
