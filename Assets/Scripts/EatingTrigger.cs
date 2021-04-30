using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingTrigger : MonoBehaviour
{
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
