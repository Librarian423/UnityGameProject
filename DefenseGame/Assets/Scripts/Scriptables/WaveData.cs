using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable/WaveData")]
public class WaveData : ScriptableObject
{
    public int wave = 0;
    public int goldDragonCount = 0;
    public int redDragonCount = 0;
    public int bossDragonCount = 0;
    public int eyeBallMinCount = 0;
    public int eyeBallMaxCount = 0;
    public bool bossWave = false;
}
