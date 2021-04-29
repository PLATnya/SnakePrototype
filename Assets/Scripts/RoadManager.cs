using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RoadManager : MonoBehaviour
{
    
    public GameObject roadPrefab;
    
    
    private float _roadLength;
    

    private ObjectPool _roadPool;
    private void Awake()
    {
        _roadPool = GetComponent<ObjectPool>();
        _roadLength = roadPrefab.transform.GetChild(0).transform.lossyScale.z;
    }

    private void Start()
    {
        _roadPool.TakeInPoolByTag();
        MakeRoad(_roadPool[0].transform,10);
    }

    public void MakeRoad(Transform startRoad, int elementsCount)
    {
        Transform road = startRoad;
        for (int i = 0; i < elementsCount; i++)
        {
            Vector3 newPosition = road.position + road.forward * _roadLength;


            GameObject newRoad = _roadPool.Generate();
            road = newRoad.transform;
            road.position = newPosition;
        }
    }
}
