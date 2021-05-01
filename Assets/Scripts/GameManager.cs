using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public static class GameManager
{
    public static Snake SnakePlayer;
    public static RoadManager RoadManager;
    public static Obstacles Obstacles;
    public static UIManager UIManager;
    public static Color[] Colors = new[]
            {Color.blue, Color.green, Color.magenta, Color.red, Color.yellow, Color.white,};


    public static int Score;

    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialization()
    {
        SnakePlayer = Object.FindObjectOfType<Snake>();
        RoadManager = Object.FindObjectOfType<RoadManager>();
        Obstacles = Object.FindObjectOfType<Obstacles>();
        UIManager = Object.FindObjectOfType<UIManager>();
    }

    public static void AddScore()
    {
        SnakePlayer.StartCoroutine(MakeNiceScore());
    }

    static IEnumerator MakeNiceScore()
    {
        for (int i = 0; i < 10; i++)
        {
            Score++;
            UIManager.SetScore(Score);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public static void Death()
    {
        UIManager.restartButton.gameObject.SetActive(true);
        SnakePlayer.isAlive = false;
    }
}
