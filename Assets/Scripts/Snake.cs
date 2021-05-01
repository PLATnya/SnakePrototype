using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Snake : MonoBehaviour
{
    [HideInInspector]
    public bool isAlive = true;
    private CharacterController _controller;
    private Transform _cameraTransform;
    private Transform _selfTransform;

    public List<Transform> tailObjects = new List<Transform>();
    
    public float speed;
    public GameObject TailPrefab;
    public Vector2 cameraZYOffset;
    public float tailPartsOffset;
    public Color snakeColor;

    public bool isStucked;
    void Start()
    {
        snakeColor = GetComponent<Renderer>().material.GetColor("Color_base");
        _selfTransform = transform;
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
        tailObjects.Add(transform);
        StandOnStart();
    }

    public void StandOnStart()
    {
        _controller.enabled = false;
        Transform startRoad = GameManager.RoadManager.startRoad;
        _selfTransform.position = startRoad.position + startRoad.forward * GameManager.RoadManager.roadLength / 2;
        _selfTransform.rotation = Quaternion.identity;
        _controller.enabled = true;
        isAlive = true;
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
            return -1;
            
        }

        return 0;
    }
    void Update()
    {
        if (isAlive)
        {
          
            Vector3 direction = Vector3.right * (WayByScreenInput());
            if (isStucked)
            {
                direction = Vector3.forward;
            }
            _selfTransform.rotation = Quaternion.Lerp(_selfTransform.rotation, Quaternion.LookRotation(direction), Time.deltaTime*speed/2f);
            _controller.SimpleMove(_selfTransform.forward*speed);
            CameraFollowing();
            TailFollowing();
        }
    }
    

    public void CameraFollowing()
    {
        _cameraTransform.position = new Vector3(_cameraTransform.position.x,
            _selfTransform.position.y + cameraZYOffset.y, _selfTransform.position.z + cameraZYOffset.x);
        
    }

    void TailFollowing()
    {
        Transform lastTailPart = tailObjects[0];
        for (int i = 1; i < tailObjects.Count; i++) 
        {
            
            Transform currentTailTransform = tailObjects[i];
            currentTailTransform.LookAt(lastTailPart);
            Vector3 targetPosition = (lastTailPart.position - lastTailPart.forward * tailPartsOffset);
            
            Vector3 velocity = Vector3.zero;
            currentTailTransform.position = Vector3.SmoothDamp(currentTailTransform.position, targetPosition, ref velocity, 0.015f);
           
            lastTailPart = currentTailTransform;
            
        }
    }
    public void AddTail()
    {
        Transform lastTailTransform = tailObjects[tailObjects.Count-1];
        GameObject newTailPart = Instantiate(TailPrefab, lastTailTransform.position - lastTailTransform.forward * tailPartsOffset,
            Quaternion.identity);
        tailObjects.Add(newTailPart.transform);
        Renderer tailPartRenderer = newTailPart.GetComponent<Renderer>();
        tailPartRenderer.material.SetColor("Color_base",snakeColor);
        tailPartRenderer.material.SetColor("Color_cover",snakeColor);
    }

    public void ClearTail()
    {
        Transform[] tail = tailObjects.ToArray();
        foreach (Transform tailPart in tail)
        {
            if (tailPart != transform)
            {
                tailObjects.Remove(tailPart);
                Destroy(tailPart.gameObject);
            }
        }
    }

}
