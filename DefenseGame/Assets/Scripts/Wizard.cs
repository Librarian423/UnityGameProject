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
    public GameObject rangeArea;

    private Animator animator;
    private AttackType type;
    private float attackSpeed = 0f;
    private float damage = 20f;
    private float attackDistance = 10f;
    private GameObject attackPrefab;
    private float attackTimer = 0f;

    private GameObject magic;

    private bool isClick = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        //Init stats
        SetStats();

        //Instantiate magic
        switch (type)
        {
            case AttackType.Single:
                break;
            case AttackType.Multiple:
                magic = Instantiate(attackPrefab, transform.position, Quaternion.identity);
                magic.GetComponent<MultipleAttack>().Damage = damage;
                magic.GetComponent<MultipleAttack>().Speed = attackSpeed;
                magic.GetComponent<MultipleAttack>().AttackTimer = attackTimer;
                magic.transform.position = new Vector3(0, 0, 0);
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
        
        if (isClick && Input.GetMouseButtonDown(0))
        {
            Attack();
            InitAfterAttack();
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("Attack", false);
            GameManager.instance.IsArrowAble = true;
        }

        if (rangeArea.active) 
        {
            SetRangePosition();
        }
    }

    public void OnClickButton()
    {
        magic.SetActive(true);
        isClick = true;
        GameManager.instance.IsArrowAble = false;
        Debug.Log("fire");
    }

    private void Attack()
    {
        animator.SetBool("Attack", true);

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        switch (type)
        {
            case AttackType.Single:
                break;
            case AttackType.Multiple:
                magic.GetComponent<MultipleAttack>().Attack(pos);
                break;
            case AttackType.Area:
                magic.GetComponent<AreaAttack>().Attack(pos);
                break;
        }
    }

    private void SetRangePosition()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        switch (type)
        {
            case AttackType.Single:
                break;
            case AttackType.Multiple:
                rangeArea.transform.position = pos;
                break;
            case AttackType.Area:
                rangeArea.transform.position = new Vector3(pos.x, 0, 0);
                break;
        }
    }

    private void InitAfterAttack()
    {
        isClick = false;
        rangeArea.SetActive(false);
        //animator.SetBool("Attack", false);
    }

    private void SetStats()
    {
        type = stats.type;
        attackSpeed = stats.attackSpeed;
        damage = stats.damage;
        attackDistance = stats.attackDistance;
        attackPrefab = stats.attackPrefab;
        attackTimer = stats.attackHitTime;
    }
}

