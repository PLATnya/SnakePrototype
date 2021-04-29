using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private bool _isAlive = true;
    private CharacterController _controller;
    public float speed;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlive)
        {
            
        }
    }
}
