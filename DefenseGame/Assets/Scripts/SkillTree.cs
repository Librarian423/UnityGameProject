using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    //public Scrollbar scrollbar;
    public GameObject FireblastWizard;
    public GameObject LaserWizard;
    public GameObject archerMain;

    public SkillTreeData data;

    private Magics magics;

    public List<Magics> enabledMagic = new List<Magics>();

    // Start is called before the first frame update
    void Start()
    {
        //scrollbar.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //FireBlast
    public void PurchaseMagic1(Button button)
    {
        bool isExist = FindIfPossible(Magics.FireBlast);
        if (isExist)
        {
            return;
        }

        if (PropertyManager.instance.Money >= data.FBWizard1Cost)
        {
            PropertyManager.instance.SetMoney(-data.FBWizard1Cost);
            FireblastWizard.SetActive(true);
            enabledMagic.Add(Magics.FireBlast);
            UIManager.instance.EnableMagicBtn(Magics.FireBlast);
            UIManager.instance.SetInterectable(button);
        }
    }

    //Laser
    public void PurchaseMagic2(Button button)
    {
        bool isExist = FindIfPossible(Magics.Laser);
        if (isExist)
        {
            return;
        }

        if (PropertyManager.instance.Money >= data.LWizard1Cost)
        {
            PropertyManager.instance.SetMoney(-data.LWizard1Cost);
            LaserWizard.SetActive(true);
            enabledMagic.Add(Magics.Laser);
            UIManager.instance.EnableMagicBtn(Magics.Laser);
            UIManager.instance.SetInterectable(button);
        }
    }

    public void PurchaseArrow1(Button button)
    {
        if (PropertyManager.instance.Money >= data.Archer1Cost)
        {
            PropertyManager.instance.SetMoney(-data.Archer1Cost);
            archerMain.GetComponentInChildren<Bow>().SetLevel2();
            UIManager.instance.SetInterectable(button);
        }
        
    }

    private bool FindIfPossible(Magics type)
    {
        return enabledMagic.Contains(type);
    }
}
