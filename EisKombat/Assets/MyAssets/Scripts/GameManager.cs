using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool GameOver;

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

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.H))
        {
            SetIG(true);
        }
    }

    public void SetGO(bool g)
    {
        GameOver = g;
        SceneManager.LoadScene("GameOver");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetIG(bool g)
    {
        GameOver = g;
        SceneManager.LoadScene("Game");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
