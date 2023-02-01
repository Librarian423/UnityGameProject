using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceData", menuName = "Scriptable/ResourceData")]
public class ResourceData : ScriptableObject
{
    public int meat = 100;
    public int gold = 100;
    public float duration = 5f;
}
