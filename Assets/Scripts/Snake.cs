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
    private float MaxTailScale;

    public List<Transform> tailObjects = new List<Transform>();
    
    public float speed;
    public float rotationSpeed;
    public float tailAddingScale = 0.2f;
    public GameObject TailPrefab;
    public Vector2 cameraZYOffset;
    public float tailPartsOffset;
    public Color snakeColor;

    [HideInInspector]
    public bool isStucked;

    
    [HideInInspector]public bool boost;
    void Start()
    {
        snakeColor = GetComponent<Renderer>().material.GetColor("Color_base");
        _selfTransform = transform;
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
        tailObjects.Add(transform);
        MaxTailScale = TailPrefab.transform.lossyScale.x;
        StandOnStart();
    }

    public void StandOnStart()
    {
        _controller.enabled = false;
        Transform startRoad = GameManager.RoadManager.startRoad;
        _selfTransform.position = startRoad.position + startRoad.forward * GameManager.RoadManager.roadLength / 2 + Vector3.up;
        _selfTransform.rotation = Quaternion.identity;
        _controller.enabled = true;
        isAlive = true;
        boost = false;
    }

    

    private Vector3 _direction = Vector3.forward;
    void FixedUpdate()
    {
        if (isAlive)
        {
            _direction = Vector3.forward;
            if (Input.touchCount > 0)
            {
                Ray inputRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(inputRay, out hit))
                {
                    Vector3 directionVector = new Vector3(hit.point.x, _selfTransform.position.y, hit.point.z) -
                                              _selfTransform.position;
                    if (directionVector.magnitude > 0.2f)
                    {
                        _direction = (directionVector)
                            .normalized;

                        if (hit.point.z <= _selfTransform.position.z)
                        {
                            _direction = new Vector3(_direction.x, _direction.y, -_direction.z);
                        }
                    }
                }
            }
            if (isStucked)
            {
                _direction = Vector3.forward;
            }

            float allSpeed = speed;
            if (boost)
            {
                _direction = new Vector3(GameManager.RoadManager.startRoad.position.x,_selfTransform.position.y, _selfTransform.position.z + GameManager.RoadManager.startRoad.forward.z)
                    -_selfTransform.position;
                allSpeed *= 3;
            }
            
            _selfTransform.rotation = Quaternion.Lerp(_selfTransform.rotation, Quaternion.LookRotation(_direction), 0.02f*speed*rotationSpeed);
            _controller.SimpleMove(_selfTransform.forward*allSpeed);
            
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
           
            //monster.position += (player.position - monster.position).normalized * speed * Time.deltaTime
            lastTailPart = currentTailTransform;
            
        }
    }
    public void AddTail()
    {
        Transform lastTailTransform = tailObjects[tailObjects.Count-1];
        if (tailObjects.Count > 1 && lastTailTransform.localScale.x < MaxTailScale)
        {
            lastTailTransform.localScale += new Vector3(tailAddingScale, tailAddingScale, tailAddingScale);
        }
        else{   
            GameObject newTailPart = Instantiate(TailPrefab,
                lastTailTransform.position - lastTailTransform.forward * tailPartsOffset,
                Quaternion.identity);
            Transform newTailPartTransform = newTailPart.transform;
            newTailPartTransform.localScale = Vector3.zero;
            tailObjects.Add(newTailPartTransform);
            Renderer tailPartRenderer = newTailPart.GetComponent<Renderer>();
            tailPartRenderer.material.SetColor("Color_base",snakeColor);
            tailPartRenderer.material.SetColor("Color_cover",snakeColor);
        }
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
