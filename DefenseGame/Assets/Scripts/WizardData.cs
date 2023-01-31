using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WizardData", menuName = "Scriptable/WizardData")]
public class WizardData : ScriptableObject
{
    public Wizard.AttackType type;
    public float attackSpeed = 0f;
    public float damage = 20f;
    public float attackDistance = 10f;
    public GameObject attackPrefab;
    public float attackHitTime = 0.2f;
}
