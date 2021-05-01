using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    
    private ObjectPool _roadPool;
    
    
    private void Awake()
    {
        _roadPool = GetComponent<ObjectPool>();
        roadLength = roadPrefab.transform.GetChild(0).lossyScale.z;
        roadWidth = roadPrefab.transform.GetChild(0).lossyScale.x;
        roadHeight = roadPrefab.transform.GetChild(0).lossyScale.y;
    }

    public void InitCameraAbove()
    {
        Transform cameraTransform = Camera.main.transform;
        cameraTransform.position = new Vector3(_roadPool[0].transform.position.x + cameraXOffset,
            cameraTransform.position.y, cameraTransform.position.z);
    }
    
    private void Start()
    {

        _roadPool.TakeInPoolByTag();
        InitCameraAbove();
        MakeRoad(_roadPool[0].transform,10);
    }

    public void MakeRoad(Transform startRoad, int elementsCount)
    {
        Transform road = startRoad;
        for (int i = 0; i < elementsCount; i++)
        {
            Vector3 newPosition = road.position + road.forward * roadLength;
            GameObject newRoad = _roadPool.Generate();
            road = newRoad.transform;
            road.position = newPosition;
            GameManager.Obstacles.GenerateObstacles(road.transform,Resources.Load<ScriptableObstacles>("Scriptables/Roads/First"));
        }
    }
}
