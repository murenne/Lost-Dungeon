using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header ("»Ø¸´Ò©Æ¿")]
    public int HPBottleNum;
    public GameObject HPBottleHealing;

    [Header("½ð±Ò")]
    public int GoldCoinNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UseBottle();
    }

    void UseBottle()
    {
        if (HPBottleNum > 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                HPBottleNum--;
                Instantiate(HPBottleHealing, transform.position, Quaternion.identity);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>().PlayerCurrentHP += 40;


            }

        
        }
    
    }
}
