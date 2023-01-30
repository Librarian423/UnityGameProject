using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Wizard : MonoBehaviour
{
    public enum AttackType
    {
        None = -1,
        Single,
        Multiple,
        Area,
    }

    public WizardData stats;

    private AttackType type;
    private float attackSpeed = 0f;
    private float damage = 20f;
    private float attackDistance = 10f;
    private GameObject attackPrefab;

    private GameObject magic;
   

    // Start is called before the first frame update
    void Start()
    {
        //Init stats
        SetStats();
        switch (type)
        {
            case AttackType.Single:
                break;
            case AttackType.Multiple:
                break;
            case AttackType.Area:
                magic = Instantiate(attackPrefab, transform.position, Quaternion.identity);
                magic.GetComponent<AreaAttack>().Damage = damage;
                magic.transform.position = new Vector3(attackDistance, 0, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPause)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            
            magic.SetActive(true);
            magic.GetComponent<AreaAttack>().Attack();

        }
    }

    private void SetStats()
    {
        type = stats.type;
        attackSpeed = stats.attackSpeed;
        damage = stats.damage;
        attackDistance = stats.attackDistance;
        attackPrefab = stats.attackPrefab;
    }
}

