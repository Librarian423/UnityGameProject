using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Wizard : MonoBehaviour
{
    public enum AttackType
    {
        None = -1,
        Single,
        Multiple,
        Area,
    }
    public AudioClip castClip;
    public WizardData stats;
    public GameObject rangeArea;

    //cool time
    public UnityEngine.UI.Slider slider;
    //private Coroutine coroutine;
    public float coolTime = 1f;
    private float timer = 0f; 

    private Animator animator;
    private AttackType type;
    private float attackSpeed = 0f;
    private float damage = 20f;
    private float attackDistance = 10f;
    private GameObject attackPrefab;
    private float attackTimer = 0f;
    private GameObject magic;
    private bool isClick = false;
    private UnityEngine.UI.Button button;

    // Start is called before the first frame update
    void Start()
    {
        timer = coolTime;
        slider.value = 0;
        slider.fillRect.gameObject.SetActive(false);
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
                //magic.transform.position = new Vector3(0, 0, 0);
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

        timer += Time.deltaTime;
        if (button != null)
        {
            if (slider.value > 0)
            {
                slider.value -= Time.deltaTime;
            }
            else
            {
                slider.fillRect.gameObject.SetActive(false);
                UIManager.instance.SetInterectableTrue(button);
            }
        }

        if (isClick && Input.GetMouseButtonDown(0))
        {
            Attack();
            InitAfterAttack();
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("Attack", false);
            
        }

        if (rangeArea.activeSelf) 
        {
            SetRangePosition();
        }
    }

    public void OnClickButton(UnityEngine.UI.Button button)
    {

        if (GameManager.instance.IsPause) 
        {
            return;
        }
        this.button = button;
        rangeArea.SetActive(true);
        isClick = true;
        slider.maxValue = coolTime;
        slider.value = 0;
        //slider.gameObject.SetActive(false);
        GameManager.instance.IsArrowAble = false; 
    }

    private void Attack()
    {
        slider.fillRect.gameObject.SetActive(true);
        UIManager.instance.SetInterectable(button);
        animator.SetBool("Attack", true);
        SoundManager.instance.PlayEffect(castClip);
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
                rangeArea.transform.position = pos;
                break;
        }
    }

    private void InitAfterAttack()
    {
        timer = coolTime;
        isClick = false;
        rangeArea.SetActive(false);
        GameManager.instance.IsArrowAble = true;
        slider.value = coolTime;
        slider.fillRect.gameObject.SetActive(true);
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

