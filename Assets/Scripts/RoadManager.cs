using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    private List<GameObject> _roadsList;
    
    public GameObject roadPrefab;

    
    [SerializeField] private int roadPoolSize;
    private float _roadLength;
    
    
    private void Awake()
    {
        _roadsList = new List<GameObject>(roadPoolSize); 
        
        _roadLength = roadPrefab.transform.GetChild(0).transform.lossyScale.z;
    }

    private void Start()
    {
        _roadsList.Add(GameObject.FindGameObjectWithTag("Road"));
        MakeRoad(_roadsList[0].transform,10);
    }

    public void MakeRoad(Transform startRoad, int elementsCount)
    {
        Transform road = startRoad;
        for (int i = 0; i < elementsCount; i++)
        {
            Vector3 newPosition = road.position + road.forward * _roadLength;
            
            if (_roadsList.Count < roadPoolSize)
            {
                road = Instantiate(roadPrefab, newPosition, Quaternion.identity)
                    .transform;
                _roadsList.Add(road.gameObject);
            }
            else
            {
                GameObject tmp = _roadsList[0];
                tmp.transform.position = newPosition;
                _roadsList.Remove(tmp);
                _roadsList.Add(tmp);
                road = tmp.transform;
            }
        }
    }
}
