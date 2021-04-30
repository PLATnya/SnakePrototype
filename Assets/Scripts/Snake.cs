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

    
    void Update()
    {
        if (_isAlive)
        {
            CameraFollowing();
        }
    }

    public void CameraFollowing()
    {
        _controller.SimpleMove((Vector3.forward + Vector3.right * Input.GetAxis("Horizontal")) * speed);
        _cameraTransform.position = new Vector3(_cameraTransform.position.x,
            _selfTransform.position.y + cameraZYOffset.y, _selfTransform.position.z + cameraZYOffset.x);
        
    }
}
