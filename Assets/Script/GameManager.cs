// GameManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject winPanel;
    public GameObject losePanel;

    private bool gameOver = false;

    void Awake() => Instance = this;

    public void GameOver()
    {
        if (gameOver) return;
        gameOver = true;
        Time.timeScale = 0;
        losePanel?.SetActive(true);
    }

    public void Victory()
    {
        if (gameOver) return;
        gameOver = true;
        winPanel?.SetActive(true);
    }

    public void PauseGame() => Time.timeScale = 0;
    public void ResumeGame() => Time.timeScale = 1;

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}