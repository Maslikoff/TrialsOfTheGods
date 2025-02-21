using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OpenOptions()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
