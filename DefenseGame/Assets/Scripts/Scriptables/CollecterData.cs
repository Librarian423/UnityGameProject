using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollecterData", menuName = "Scriptable/CollecterData")]
public class CollecterData : ScriptableObject
{
    public int maxGold = 10;
    public int maxMeat = 10;
    public float speed = 1f;
    public float collectSpeed = 1f;
}
