using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public static class GameManager
{
    public static Snake SnakePlayer;
    public static RoadManager RoadManager;
    public static Obstacles Obstacles;
    public static Color[] Colors = new[]
            {Color.blue, Color.green, Color.magenta, Color.red, Color.yellow, Color.white,};
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialization()
    {
        SnakePlayer = Object.FindObjectOfType<Snake>();
        RoadManager = Object.FindObjectOfType<RoadManager>();
        Obstacles = Object.FindObjectOfType<Obstacles>();
    }
}
