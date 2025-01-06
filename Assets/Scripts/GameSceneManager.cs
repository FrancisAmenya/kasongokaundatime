using UnityEngine;

public class GameSceneManager : MonoBehaviour
{

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ReturnToMenu()
    {
        ResumeGame();
        sceneController.LoadMenuScene();
    }
    
    public void OpenSettings()
    {
        // Implement settings panel logic
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
