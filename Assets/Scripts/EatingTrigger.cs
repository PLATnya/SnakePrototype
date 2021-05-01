using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            GameManager.AddScore();
            Destroy(other.gameObject);
        }else if (other.CompareTag("Spike"))
        {
            GameManager.SnakePlayer.isAlive = false;
        }else if (other.CompareTag("Human"))
        {
            if (other.GetComponent<Renderer>().material.color == GameManager.SnakePlayer.snakeColor)
            {
                GameManager.SnakePlayer.AddTail();
                Destroy(other.gameObject);
            }
            else
            {
                GameManager.SnakePlayer.isAlive = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Border"))
        {
            
            GameManager.SnakePlayer.isStucked = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Border"))
        {
            
            GameManager.SnakePlayer.isStucked = false;
        }
    }
}
