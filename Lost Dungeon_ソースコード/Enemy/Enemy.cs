using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakenDamage
{
    [SerializeField] private float moveSpeed;
    private Transform target;
    [SerializeField] private int maxHp;
    public int hp;

    [Header("Hurt")]
    private SpriteRenderer sp;
    public float hurtLength;//MARKER 效果持续多久
    private float timeBtwHurt;//MARKER 相当于计数器

    [HideInInspector] public bool isAttacked;
    [SerializeField] private GameObject DeathEffect;

    public bool isAttack { get { return isAttacked; }  set { isAttacked = value; } }

    // public GameObject bulletEffect;

    public float attackRange;
    RoomManagement RM;

    public GameObject[] DropItem;


    private void Start() 
    {
        hp = maxHp;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();
        RM = GameObject.Find("RoomManagement").GetComponent<RoomManagement>();
    }

    private void Update()
    {
        FollowPlayer();

        timeBtwHurt -= Time.deltaTime;
        if (timeBtwHurt <= 0)
            sp.material.SetFloat("_FlashAmount", 0);
    }

    private void FollowPlayer()
    {
        if (Vector2.Distance(transform.position, target.position) >= attackRange)
        {
            //anim.SetBool("isAttack", false);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            if (target.position.x > transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }

        }
    }

    public void TakenDamage(int _amount)
    {
        isAttack = true;
        StartCoroutine(isAttackCo());
        hp -= _amount;
        HurtEffect();

        if (hp <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().AddExp(8);
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            //掉落物品
            var temp = Random.Range(1, 100);
            if (temp <= 5)
            {
                Instantiate(DropItem[0], transform.position, Quaternion.identity);
            }
            else if (temp <= 20)
            {
                Instantiate(DropItem[1], transform.position, Quaternion.identity);
            }
            else if (temp <= 50)
            {
                Instantiate(DropItem[2], transform.position, Quaternion.identity);
            }

        }
    }

    private void HurtEffect() 
    {
        sp.material.SetFloat("_FlashAmount", 1);
        timeBtwHurt = hurtLength;
    }

    IEnumerator isAttackCo()
    {
        yield return new WaitForSeconds(0.2f);
        isAttack = false;
    }

    private void OnBecameVisible()
    {
        RM.EnemyNum++;
    }
    private void OnBecameInvisible()
    {
        RM.EnemyNum--;
    }

}
