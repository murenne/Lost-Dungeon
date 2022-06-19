using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{

    private int AttackDamage;

    public GameObject DamageCanvas;

    [Header("Ëæ»úÉËº¦·¶Î§")]
    [SerializeField] private float MinDamage;
    [SerializeField] private float MaxDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ITakenDamage enemy = collision.gameObject.GetComponent<ITakenDamage>();

            AttackDamage = (int)Random.Range(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().ATS - MinDamage, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().ATS + MaxDamage);

            if (!enemy.isAttack)//collision.gameObject.GetComponent<EnemyControl>().IsAttacked == false)
            {
                //collision.gameObject.GetComponent<EnemyControl>().TakenDamage(AttackDamage);

                enemy.TakenDamage(AttackDamage);
                FindObjectOfType<CameraControl>().CameraShake(0.4f);
                DamageNum Damagable = Instantiate(DamageCanvas, collision.transform.position, Quaternion.identity).GetComponent<DamageNum>();
                Damagable.ShowUIDamage(Mathf.RoundToInt(AttackDamage));

                Vector2 difference = collision.transform.position - transform.position;
                difference.Normalize();
                collision.transform.position = new Vector2(collision.transform.position.x + difference.x / 2, collision.transform.position.y + difference.y / 2);
            }
        }

    }

    public void EndAttack()
    {
        gameObject.SetActive(false);

    }

}
