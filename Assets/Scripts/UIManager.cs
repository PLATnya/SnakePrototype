using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Button restartButton;
    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    private void Start()
    {
        SetScore(0);
        restartButton.gameObject.SetActive(false);
    }
     public void Restart()
     {
         SceneManager.LoadScene(0);
      }
}
