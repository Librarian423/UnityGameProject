using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "DragonData", menuName = "Scriptable/DragonData")]
public class DragonData : ScriptableObject
{
    public float health = 100f;
    public float speed = 2f;
    public float damage = 10f;
    public float movePos = 10f;
    public float fireDelay = 2f;
    public int dropGold = 100;
    public GameObject hitPrefab;
    public float playDuration = 1f;

    public void PlayEffect(Vector2 position)
    {
        if (hitPrefab == null)
            return;

        var hiteffect = Instantiate(hitPrefab, position, Quaternion.identity);
        Destroy(hiteffect, playDuration);
    }
}