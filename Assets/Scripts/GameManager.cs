using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public GameObject gameOverPanel;
    public GameObject winText;
    public GameObject gameOverText;
    private int enemiesAlive = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        Time.timeScale = 1.0f;  
    }

    private void Start()
    {
        CountEnemiesInScene();
        gameOverPanel.SetActive(false);
    }

    public void CountEnemiesInScene()
    {
        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            WinGame();
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverText.SetActive(true);
        winText.SetActive(false);
    }

    public void WinGame()
    {
        gameOverPanel.SetActive(true);
        gameOverText.SetActive(false);
        winText.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
