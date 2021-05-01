using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    private void Start()
    {
        SetScore(0);
    }
}
