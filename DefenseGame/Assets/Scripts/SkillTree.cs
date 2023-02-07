using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    //public Scrollbar scrollbar;
    [Header("Characters")]
    public GameObject FireblastWizard;
    public GameObject ExplosionWizard;
    public GameObject LaserWizard;
    public GameObject ElectWizard;
    public GameObject archerMain;

    [Header("Cost of Each Skills")]
    public SkillTreeData data;

    [Header("Skill Buttons")]
    public Button[] FlameTree;
    public Button[] LaserTree;
    public Button[] ArcherTree;

    //private Magics magics;

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
    public void PurchaseMagic1()
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
            UIManager.instance.SetInterectable(FlameTree[0]);
            SetNextSkillInterectable(FlameTree[1]);
        }
    }

    public void PurchaseMagic1_1()
    {
        bool isExist = FindIfPossible(Magics.Explosion);
        if (isExist)
        {
            return;
        }

        if (PropertyManager.instance.Money >= data.FBWizard2Cost)
        {
            PropertyManager.instance.SetMoney(-data.FBWizard2Cost);
            ExplosionWizard.SetActive(true);
            enabledMagic.Add(Magics.Explosion);
            UIManager.instance.EnableMagicBtn(Magics.Explosion);
            UIManager.instance.SetInterectable(FlameTree[1]);

        }
    }

    //Laser
    public void PurchaseMagic2()
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
            UIManager.instance.SetInterectable(LaserTree[0]);
            SetNextSkillInterectable(LaserTree[1]);
        }
    }

    public void PurchaseMagic2_1()
    {
        bool isExist = FindIfPossible(Magics.Electric);
        if (isExist)
        {
            return;
        }

        if (PropertyManager.instance.Money >= data.LWizard2Cost)
        {
            PropertyManager.instance.SetMoney(-data.LWizard2Cost);
            ElectWizard.SetActive(true);
            enabledMagic.Add(Magics.Electric);
            UIManager.instance.EnableMagicBtn(Magics.Electric);
            UIManager.instance.SetInterectable(LaserTree[1]);
            //SetNextSkillInterectable(LaserTree[1]);
        }
    }

    public void PurchaseArrow1()
    {
        if (PropertyManager.instance.Money >= data.Archer1Cost)
        {
            PropertyManager.instance.SetMoney(-data.Archer1Cost);
            archerMain.GetComponentInChildren<Bow>().SetLevel2();
            UIManager.instance.SetInterectable(ArcherTree[0]);
            SetNextSkillInterectable(ArcherTree[1]);
        }
        
    }

    public void PurchaseArrow2()
    {
        if (PropertyManager.instance.Money >= data.Archer2Cost)
        {
            PropertyManager.instance.SetMoney(-data.Archer2Cost);
            archerMain.GetComponentInChildren<Bow>().SetLevel3();
            UIManager.instance.SetInterectable(ArcherTree[1]);
        }

    }

    private bool FindIfPossible(Magics type)
    {
        return enabledMagic.Contains(type);
    }

    private void SetNextSkillInterectable(Button button)
    {
        if (button != null)
        {
            UIManager.instance.SetInterectableTrue(button);
        }
        
    }
}
