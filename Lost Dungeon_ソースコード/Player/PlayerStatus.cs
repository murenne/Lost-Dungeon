using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("��ɫ�ȼ�")]
    public int PlayerLevel;
    public int MaxLevel;

    [Header("��ɫ����")]
    public int CurrentExp;
    public int[] NextLevelExp;

    [Header("������ֵ")]
    public int Attack;
    public int Defense;
    public int ATS;
    public int ADF;

    [Header("��ֵ��ֵ")]
    public int CurrATK;
    public int PreATK;
    public int CurrDEF;
    public int PreDEF;
    public int CurrATS;
    public int PreATS;
    public int CurrADF;
    public int PreADF;

    public GameObject PlayerLevelUpEffect;

    private void Awake()
    {
        NextLevelExp = new int[MaxLevel + 1];
        NextLevelExp[1] = 300;

        for (int i = 2; i < MaxLevel; i++)
        {
            NextLevelExp[i] = Mathf.RoundToInt(NextLevelExp[i - 1] * 1.1f);
        }

        CurrATK = Attack;
        CurrDEF = Defense;
        CurrATS = ATS;
        CurrADF = ADF;

        PreATK = 0;
        PreDEF = 0;
        PreATS = 0;
        PreADF = 0;



    }
    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Space))
        //{
            //AddExp(50);     //����
       // }
    }


    public void AddExp(int amount)  //�ò�������ͬ����
    {
        PreATK = Attack;
        PreDEF = Defense;
        PreATS = ATS;
        PreADF = ADF;


        CurrentExp += amount;
        if (CurrentExp >= NextLevelExp[PlayerLevel] && PlayerLevel < MaxLevel)
        {
            LevelUp();
            Instantiate(PlayerLevelUpEffect, transform.position, Quaternion.identity);
        }

        if (PlayerLevel >= MaxLevel)
        {
            CurrentExp = 0;
        }
        FindObjectOfType<UIManagememt>().UpdataPlayerStatus();

    }

    private void LevelUp()
    {
        CurrentExp -= NextLevelExp[PlayerLevel];
        PlayerLevel++;

        Attack = Mathf.CeilToInt(Attack * 1.15f);
        Defense = Mathf.RoundToInt(Defense * 1.1f);
        ATS = Mathf.RoundToInt(ATS * 1.2f);
        ADF += 2;

        CurrATK = Attack;
        CurrDEF = Defense;
        CurrATS = ATS;
        CurrADF = ADF;

        FindObjectOfType<UIManagememt>().MakingAnimation();
    }


}
