using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingTrigger : MonoBehaviour
{
    private static int countGems = 0;

    IEnumerator Boost()
    {
        GameManager.SnakePlayer.boost = true;
        yield return new WaitForSeconds(3);
        GameManager.SnakePlayer.boost = false;
    }

    IEnumerator EatGem()
    {
        countGems++;

        if (countGems >= 3)
        {
            StopAllCoroutines();
            countGems = 0;
            StartCoroutine(Boost());
        }

        yield return new WaitForSeconds(3);
        countGems = 0;

    }   
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.SnakePlayer.boost)
        {
            if (other.CompareTag("Gem") || other.CompareTag("Spike") || other.CompareTag("Human"))
            {
                Destroy(other.gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Gem"))
            {
                GameManager.AddScore();
                StartCoroutine(EatGem());
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Spike"))
            {
                GameManager.Death();
            }
            else if (other.CompareTag("Human"))
            {
                if (other.GetComponent<Renderer>().material.color == GameManager.SnakePlayer.snakeColor)
                {
                    GameManager.SnakePlayer.AddTail();
                    Destroy(other.gameObject);
                }
                else
                {
                    GameManager.Death();
                }
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
