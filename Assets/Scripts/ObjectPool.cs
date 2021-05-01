using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectPool:MonoBehaviour
{
    private List<GameObject> _poolList;
    public int poolSize;
    public string tag;
    public GameObject objectExample;


    public int ActivaPoolSize
    {
        get => _poolList.Count;
    }
    private void Awake()
    {
        _poolList = new List<GameObject>();
    }
    

    public void TakeInPoolByTag()
    {
        foreach (GameObject varGameObject in GameObject.FindGameObjectsWithTag(tag))
        {
            _poolList.Add(varGameObject);
        }
    }
    public GameObject this[int key]
    {
        get => _poolList[key];
    }
    public GameObject Generate()
    {
        if (_poolList.Count < poolSize)
        {
            GameObject newObject = GameObject.Instantiate(objectExample);
            _poolList.Add(newObject);
            newObject.SetActive(true);
            return newObject;
        }
        GameObject tmp = _poolList[0];
        _poolList.Remove(tmp);
        _poolList.Add(tmp);
        tmp.SetActive(true);
        return tmp;
    }
}
