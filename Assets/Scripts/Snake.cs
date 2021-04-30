using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private bool _isAlive = true;
    private CharacterController _controller;
    private Transform _cameraTransform;
    private Transform _selfTransform;
    public float speed;
    public Vector2 cameraZYOffset;
    void Start()
    {
        _selfTransform = transform;
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
    }

    int WayByScreenInput()
    {
        if (Input.touchCount > 0)
        {
            Vector2 touchScreenPosition = Input.GetTouch(0).position;
            if (touchScreenPosition.x > Screen.width / 2)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        return 0;
    }
    void Update()
    {
        if (_isAlive)
        {
            
            Vector3 speedVector = Vector3.forward;
            speedVector+=Vector3.right*WayByScreenInput();
            _controller.SimpleMove(speedVector*speed);
            
            CameraFollowing();
        }
    }

    public void CameraFollowing()
    {
        _cameraTransform.position = new Vector3(_cameraTransform.position.x,
            _selfTransform.position.y + cameraZYOffset.y, _selfTransform.position.z + cameraZYOffset.x);
        
    }
}
