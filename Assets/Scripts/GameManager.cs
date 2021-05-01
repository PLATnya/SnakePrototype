using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public static class GameManager
{
    public static Snake SnakePlayer;
    public static RoadManager RoadManager;
    public static Obstacles Obstacles;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialization()
    {
        SnakePlayer = Object.FindObjectOfType<Snake>();
        RoadManager = Object.FindObjectOfType<RoadManager>();
        Obstacles = Object.FindObjectOfType<Obstacles>();
    }
}
