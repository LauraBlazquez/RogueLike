using System.Collections;
using System.Collections.Generic;
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

    [Header("Win Parameters")]
    private List<Enemy> enemies = new List<Enemy>();
    private bool canCheckVictory = false;

    private void Start()
    {
        StartCoroutine(EnableVicrotyCheckAfterDelay());
    }

    private IEnumerator EnableVicrotyCheckAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        canCheckVictory = true;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void RegisterEnemy(Enemy e)
    {
        if (!enemies.Contains(e))
        {
            enemies.Add(e); 
        }
    }

    public void UnregisterEnemy(Enemy e)
    {
        if (enemies.Contains(e))
        {
            enemies.Remove(e);
            if (enemies.Count == 0 && canCheckVictory)
            {
                WinGame();
            }
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
        Player.IsDead = false;
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
