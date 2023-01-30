using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DragonData", menuName = "Scriptable/DragonData")]
public class DragonData : ScriptableObject
{
    public float health = 100f;
    public float speed = 2f;
    public float damage = 10f;
    public float movePos = 10f;
    public float fireDelay = 2f;

}