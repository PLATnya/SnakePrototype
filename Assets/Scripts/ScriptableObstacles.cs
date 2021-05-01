using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstaclesEnum
{
    Spike,
    Humans,
    Gem,
    None
}

[CreateAssetMenu(fileName = "Obstacles", menuName = "Scriptable/Obstacles")]
public class ScriptableObstacles : ScriptableObject
{
    public int width;
    public ObstaclesEnum[] matrix;
}
