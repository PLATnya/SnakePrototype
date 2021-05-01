using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RoadManager : MonoBehaviour
{
    
    public GameObject roadPrefab;
    public float cameraXOffset;
    
    [HideInInspector]
    public float roadLength;
    [HideInInspector]
    public float roadWidth;
    [HideInInspector]
    public float roadHeight;
    
    [HideInInspector]
    public ObjectPool roadPool;

    private ScriptableObstacles[] roadsObstacles;

    public Transform startRoad;
    private void Awake()
    {
        roadPool = GetComponent<ObjectPool>();
        roadLength = roadPrefab.transform.GetChild(0).lossyScale.z;
        roadWidth = roadPrefab.transform.GetChild(0).lossyScale.x;
        roadHeight = roadPrefab.transform.GetChild(0).lossyScale.y;
        roadsObstacles = Resources.LoadAll<ScriptableObstacles>("Scriptables/Roads");
        
    }

    
    public void InitCameraAbove()
    {
        Transform cameraTransform = Camera.main.transform;
        cameraTransform.position = new Vector3(startRoad.position.x + cameraXOffset,
            cameraTransform.position.y, cameraTransform.position.z);
    }
    
    private void Start()
    {
        InitCameraAbove();
        MakeRoad(startRoad,10);
    }

    public void MakeRoad(Transform startRoad, int elementsCount)
    {
        Transform road = startRoad;
        for (int i = 0; i < elementsCount; i++)
        {
            Vector3 newPosition = road.position + road.forward * roadLength;
            GameObject newRoad = roadPool.Generate();
            road = newRoad.transform;
            road.position = newPosition;
            road.GetComponentInChildren<RoadTrigger>().ReloadColor();
            GameManager.Obstacles.GenerateObstacles(road.transform,roadsObstacles[Random.Range(0,roadsObstacles.Length)]);
        }
    }

    public void ClearRoads()
    {
        GameObject[] roads = GameObject.FindGameObjectsWithTag("Road");
         
        foreach (GameObject road in roads)
        {
             
            Transform rootTransform = road.transform.Find("Obstacles Root");
            if (rootTransform)
            {
                for (int i = 0; i < rootTransform.childCount; ++i)
                    rootTransform.GetChild(i).gameObject.SetActive(false);
                rootTransform.DetachChildren();
            }
            road.SetActive(false);

        }
    }
}
