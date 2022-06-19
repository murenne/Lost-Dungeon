using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagememt : MonoBehaviour
{
    public Image  HPBarPoint;
    public Image  HPBarEffect;
    public Slider  EXPBar;
    public Image MagicBar;
    public float EffectSpeed;

    public PlayerCtrl Player;

    public GameObject MiniMap;
    private bool MapActive = false;

    [Header("物品")]
    public Text HPBottleNumText;
    public Text GoldCoinNumText;

    [Header("基础数值")]
    public Text ATKText;
    public Text DEFText;
    public Text ATSText;
    public Text ADFText;

    [Header("数值变化")]
    public Text ATKDisplayText;
    public Text DEFDisplayText;
    public Text ATSDisplayText;
    public Text ADFDisplayText;


    public GameObject[] GameObjectWithAnimators;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdataPlayerStatus();
    }

    // Update is called once per frame
    void Update()
    {
        EXPBar.value = Player.GetComponent<PlayerStatus>().CurrentExp;
        EXPBar.maxValue = Player.GetComponent<PlayerStatus>().NextLevelExp[Player.GetComponent<PlayerStatus>().PlayerLevel];

        HPBarPoint.fillAmount = Player.GetComponent<PlayerCtrl>().PlayerCurrentHP / Player.GetComponent<PlayerCtrl>().PlayerMaxHP;
        MagicBar.fillAmount = Player.GetComponent<MagicAttack>().MagicAttackTime / Player.GetComponent<MagicAttack>().Timer;


        if (HPBarEffect.fillAmount > HPBarPoint.fillAmount)
        {
            HPBarEffect.fillAmount -= EffectSpeed;
        }
        else
        {
            HPBarEffect.fillAmount = HPBarPoint.fillAmount;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            MapActive = !MapActive;
            if (MapActive)
            {
                MiniMap.SetActive(true);
            }
            else
            {
                MiniMap.SetActive(false);
            }
        
        }

        HPBottleNumText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItems>().HPBottleNum.ToString();
        GoldCoinNumText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItems>().GoldCoinNum.ToString();
    }

    public void UpdataPlayerStatus()
    {
        ATKText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().Attack.ToString();
        DEFText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().Defense.ToString();
        ATSText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().ATS.ToString();
        ADFText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().ADF.ToString();
    }

    public void MakingAnimation()
    {
        for (int i = 0; i < GameObjectWithAnimators.Length; i++)
        {
            
            ATKDisplayText.text = "+" + (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().CurrATK - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().PreATK);
            DEFDisplayText.text = "+" + (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().CurrDEF - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().PreDEF);
            ATSDisplayText.text = "+" + (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().CurrATS - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().PreATS);
            ADFDisplayText.text = "+" + (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().CurrADF - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().PreADF);


           GameObjectWithAnimators[i].GetComponent<Animator>().SetTrigger("levelup");
        }
    }
}
