using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float moveSpeed;

    public GameObject destroyEffect, attackEffect;

    private float lifeTimer;
    [SerializeField] private float maxLife = 2.0f;
    public int MagicDamage;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
            Attack();

        lifeTimer += Time.fixedDeltaTime ;
        if(lifeTimer >= maxLife)
        {
           // Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        //Vector2 Direction = target.transform.position - transform.position;
       // transform.position = new Vector2(transform.position.x + Direction.x * Time.fixedDeltaTime, transform.position.y + Direction.y * Time.fixedDeltaTime);

         transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            int temp;

            temp = MagicDamage - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().ADF;
            if (temp <= 1)
            {
                temp = 1;
            }
            other.GetComponent<PlayerCtrl>().PlayerCurrentHP -= temp;
            other.GetComponent<PlayerCtrl>().GetHurtAnim();
            FindObjectOfType<CameraControl>().CameraShake(0.2f);
            Instantiate(attackEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
