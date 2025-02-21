using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangingLevel : MonoBehaviour
{
    private int _nextSceneIndex;

    private void Start()
    {
        _nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(_nextSceneIndex);
            else
                SceneManager.LoadScene("MainMenu");
        }
    }

    public void SwitchMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(_nextSceneIndex - 1);
    }
}
