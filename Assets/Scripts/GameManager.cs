using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager:MonoBehaviour
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
        SnakePlayer = FindObjectOfType<Snake>();
        RoadManager = FindObjectOfType<RoadManager>();
        Obstacles = FindObjectOfType<Obstacles>();
        UIManager = FindObjectOfType<UIManager>();
        
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

    public void Restart()
    {
        RoadManager.ClearRoads();

        SnakePlayer.ClearTail();

        SnakePlayer.StandOnStart();
        RoadManager.MakeRoad(RoadManager.startRoad, 10);
        
        UIManager.restartButton.gameObject.SetActive(false);
        Score = 0;
        UIManager.SetScore(0);

    }

    public void QUit()
    {
        Application.Quit();
    }
}
