using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool GameOver;

    int score;

    int killedEnemyAmount;

    private static GameManager instance;

    public static GameManager Get()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UIMenuCanvas.QuitGame = QuitGame;
        UIMenuCanvas.PlayGame = SetIG;
        PlaneController.GameOver = SetGO;
        EnemyBehaviour.KillEnemy += AddScore;
        EnemyBehaviour.KillEnemy += UpdateKEAmount;
    }

    void OnDestroy()
    {
        EnemyBehaviour.KillEnemy -= AddScore;
        EnemyBehaviour.KillEnemy -= UpdateKEAmount;
    }

    void AddScore()
    {
        score += 50;
    }

    void UpdateKEAmount()
    {
        killedEnemyAmount++;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetKEAmount()
    {
        return killedEnemyAmount;
    }

    public void SetGO()
    {
        GameOver = true;
        SceneManager.LoadScene("GameOver");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetIG()
    {
        GameOver = false;
        SceneManager.LoadScene("Game");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
