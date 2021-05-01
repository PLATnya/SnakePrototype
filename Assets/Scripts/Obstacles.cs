using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacles : MonoBehaviour
{
    private void Awake()
    {
        _gemsPool = GameObject.Find("GemsPool").GetComponent<ObjectPool>();
        _humansPool = GameObject.Find("HumansPool").GetComponent<ObjectPool>();
        _spikesPool = GameObject.Find("SpikesPool").GetComponent<ObjectPool>();
    }

    private ObjectPool _gemsPool;
    private ObjectPool _humansPool;
    private ObjectPool _spikesPool;
    
    public void GenerateObstacles(Transform road, ScriptableObstacles scriptableObstacles)
    {

        float chunkWidth = GameManager.RoadManager.roadWidth / scriptableObstacles.width;
        int rows = (scriptableObstacles.matrix.Length - scriptableObstacles.matrix.Length % scriptableObstacles.width)/scriptableObstacles.width;
        float chunkHeight = GameManager.RoadManager.roadLength / rows;
       

        var right = road.right;
        Vector3 origin = road.position - right * GameManager.RoadManager.roadWidth / 2;
        origin += road.forward * chunkHeight / 2 + right * chunkWidth / 2 + road.up* GameManager.RoadManager.roadHeight/2;
        
        
        GameObject obstaclesRoot = new GameObject("Obstacles Root");    
        Transform obstaclesRootTransform = obstaclesRoot.transform;
        obstaclesRootTransform.parent = road;
        obstaclesRootTransform.localPosition = Vector3.zero;
        for (int i = 0; i < scriptableObstacles.matrix.Length; ++i)
        {
            
            int row = (i - i % scriptableObstacles.width)/scriptableObstacles.width;
            int column = i - row * scriptableObstacles.width;

            GameObject newObstacle = null;
            switch (scriptableObstacles.matrix[i])
            {
                case ObstaclesEnum.Spike:
                    newObstacle = _spikesPool.Generate();
                    break;
                case ObstaclesEnum.Gem:
                    newObstacle = _gemsPool.Generate();
                    break;
                case ObstaclesEnum.Humans:
                    newObstacle = _humansPool.Generate();
                    GenerateHumansColor(newObstacle.transform);
                    break;

            }

            if (newObstacle!=null)
            {
                
                Transform newObstacleTransform = newObstacle.transform;
                newObstacleTransform.parent = obstaclesRootTransform;
                newObstacleTransform.position = origin + road.right * chunkWidth * column + road.forward*chunkHeight*row;
                
            }
        }
    }


    public void GenerateHumansColor(Transform humans)
    {
        foreach (Renderer render in humans.GetComponentsInChildren<Renderer>())
        {
            render.material.color = GameManager.Colors[Random.Range(0, GameManager.Colors.Length)];
        }
    }
}
