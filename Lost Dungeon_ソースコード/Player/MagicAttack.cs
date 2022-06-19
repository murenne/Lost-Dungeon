using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    public GameObject MagicAttactAnimation;
    public  float MagicAttackTime;
    public  float Timer = 60f;


    // Start is called before the first frame update
    void Start()
    {
        MagicAttackTime= 60f;
        Timer = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        MagicAttackTime += Time.fixedDeltaTime;
        if (MagicAttackTime > Timer)
        {
            MagicAttackTime = Timer;

            if (Input.GetMouseButtonDown(1))
            {
                MagicAttackStart();
                MagicAttackTime = 0;
            }

        }

       
    }

    private void MagicAttackStart()
    {
        var MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var MagicPositiin = new Vector3(MousePoint.x, MousePoint.y, MousePoint.z + 10);
        Instantiate(MagicAttactAnimation, MagicPositiin , Quaternion.identity);
    }
}
