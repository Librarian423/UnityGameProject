using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PropertyManager : MonoBehaviour
{

    // 싱글톤 접근용 프로퍼티
    private static PropertyManager m_instance;
    public static PropertyManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<PropertyManager>();
            }

            return m_instance;
        }
    }

    private int money = 0;
    private int meat = 0;

    //public int Money { get { return money; } set { money = value; } }
    //public int Meat { get { return meat; } set { meat = value; } }

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI meatText;

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (moneyText != null && meatText != null)
        {
            moneyText.text = money.ToString();
            meatText.text = meat.ToString();
        }
    }

    public void SetMoney(int money)
    {
        this.money += money; 
        moneyText.text = this.money.ToString();  
    }

    public void SetMeat(int meat)
    {
        this.meat += meat;
        meatText.text = this.meat.ToString();
    }

    public void SetProperty(int money, int meat)
    {
        this.money += money;
        this.meat += meat;
        moneyText.text = this.money.ToString();
        meatText.text = this.meat.ToString();
    }
}
