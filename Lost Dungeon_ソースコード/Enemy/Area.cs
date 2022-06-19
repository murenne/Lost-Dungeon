using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    // public GameObject bulletEffect;
    public int PhysicsDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" )//&& !other.gameObject.GetComponent<ITakenDamage>().isAttack)
        {
            // Instantiate(bulletEffect, transform.position, Quaternion.identity);
            // FindObjectOfType<CameraController>().CameraShake(0.5f);

            //ITakenDamage damageable = other.gameObject.GetComponent<ITakenDamage>();
            // damageable.TakenDamage(1);
            int temp;

            temp = PhysicsDamage - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().Defense;
            if (temp <= 1)
            {
                temp = 1;
            }
            other.GetComponent<PlayerCtrl>().PlayerCurrentHP -= temp ;
            other.GetComponent<PlayerCtrl>().GetHurtAnim();
            FindObjectOfType<CameraControl>().CameraShake(0.2f);
        }
    }
}
