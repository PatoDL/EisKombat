using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverCanvas : UIMenuCanvas
{
    public Text scoreText;
    public Text keaText;

    void Start()
    {
        scoreText.text = "Score: " + GameManager.Get().GetScore();
        keaText.text = "You Killed " + GameManager.Get().GetKEAmount() + " enemies";
    }
}
