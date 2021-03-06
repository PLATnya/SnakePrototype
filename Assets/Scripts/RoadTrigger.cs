using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoadTrigger : MonoBehaviour
{
    public Color newColor;

    [SerializeField]
    private Renderer checkPoint;
    private void OnTriggerEnter(Collider other)
    {
        GameManager.SnakePlayer.snakeColor = newColor;
        if (other.CompareTag("Player"))
        {
            foreach (Transform tailObject in GameManager.SnakePlayer.tailObjects)
            {
                Renderer tailRender = tailObject.GetComponent<Renderer>();
                tailRender.material.SetVector("Vector3_pos", transform.position);
                tailRender.material.SetColor("Color_cover",newColor);
            }
            
        }
    }

    
    public void ReloadColor()
    {
        newColor = GameManager.Colors[Random.Range(0, GameManager.Colors.Length)];
        checkPoint.material.color = newColor;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Tail"))
        {
            Renderer tailRender = other.GetComponent<Renderer>();
            tailRender.material.SetColor("Color_base", tailRender.material.GetColor("Color_cover"));
        }
    }
}
