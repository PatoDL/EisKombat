using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuCanvas : MonoBehaviour
{
    public delegate void OnQuitGame();
    public static OnQuitGame QuitGame;

    public delegate void OnPlayGame();
    public static OnPlayGame PlayGame;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        PlayGame();
    }

    public void Quit()
    {
        QuitGame();
    }
}
