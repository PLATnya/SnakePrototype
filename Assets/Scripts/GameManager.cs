using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
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
        Score++;
        UIManager.SetScore(Score);
    }

}
