using System.Collections;
using UnityEngine;

public class EnemyControl : MonoBehaviour, ITakenDamage
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Transform target;
    private Animator anim;

    public GameObject projectile;
    public Transform firePoint;

    public float EmenyHP;
    public float EmanyMaxHP;

    [Header("受伤Shader")]
    private SpriteRenderer Sp;
    public float HurtLength; // 颜色变化持续时间
    private float HurtCounter;//计数器


    [SerializeField] private float ShotTimer;
    [SerializeField] private float MaxShotTime = 5.0f;

    [SerializeField] private float moveSpeed;
    public float attackRange;

    RoomManagement RM;
    [SerializeField] private GameObject DeathEffect;

    [HideInInspector] public bool isAttacked;
    public GameObject[] DropItem;

    public int PhysicsDamage;

    public bool isAttack { get { return isAttacked; } set { isAttacked = value; } }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EmenyHP = EmanyMaxHP;
        Sp = GetComponent<SpriteRenderer>();
        RM = GameObject.Find("RoomManagement").GetComponent<RoomManagement>();
    }

    // Update is called once per frame
    void Update()
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

            ShotTimer += Time.fixedDeltaTime;

            if (ShotTimer >= MaxShotTime)
            {
                Shot();
                ShotTimer = 0;
            }

            // anim.SetBool("isAttack", true);
        }

        if (HurtCounter <= 0)
        {
            Sp.material.SetFloat("_FlashAmount", 0);
        }
        else
        {
            HurtCounter -= Time.fixedDeltaTime;
        }
    }


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
    public void Shot()
    {

        Instantiate(projectile, firePoint.position, Quaternion.identity);
    }

    
    private void HurtShader()
    {
        Sp.material.SetFloat("_FlashAmount", 1);
        HurtCounter = HurtLength;
    }

    IEnumerator IsAttackCo()
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

    public void TakenDamage(int _amount)

    {
        isAttack = true;
        StartCoroutine(IsAttackCo());
        EmenyHP -= _amount;
        HurtShader();

        if (EmenyHP <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().AddExp(12);
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            //掉落物品
            var temp = Random.Range(1, 100);
            if (temp <= 5)
            {
                Instantiate(DropItem[0], transform.position, Quaternion.identity);
            }
            else if (temp > 5 && temp <= 30)
            {
                Instantiate(DropItem[1], transform.position, Quaternion.identity);
            }
            else if (temp > 30 && temp <= 35)
            {
                Instantiate(DropItem[2], transform.position, Quaternion.identity);
            }
        }
    }
}



