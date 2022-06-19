using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSword : MonoBehaviour
{
    public int PhysicsDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            int temp;

            temp = PhysicsDamage - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().Defense;
            if (temp <= 1)
            {
                temp = 1;
            }
            other.GetComponent<PlayerCtrl>().PlayerCurrentHP -= temp; ;
            other.GetComponent<PlayerCtrl>().GetHurtAnim();
            // Instantiate(attackEffect, transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }
    }
}
