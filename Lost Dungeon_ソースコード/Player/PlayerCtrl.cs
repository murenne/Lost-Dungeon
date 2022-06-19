using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance;//����ģʽ

    [Header("�ƶ�")]
    private Rigidbody2D Rb;
    private Animator Anim;
    private float MoveH, MoveV;
    public float MoveSpeed;

    [Header("������ֵ")]
    public float PlayerMaxHP;
    public float PlayerCurrentHP;

    private void Awake()
    {
        if (instance == null)   //����ģʽ
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);//��֤�ڳ���ת���иýű��������κγ�����

        PlayerCurrentHP = PlayerMaxHP;
    }

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Flip();

        if (PlayerCurrentHP > PlayerMaxHP)
        {
            PlayerCurrentHP = PlayerMaxHP;
        }
    }
    private void FixedUpdate()
    {
        Rb.velocity = new Vector2(MoveH, MoveV);
    }

    void Movement()
    {
        MoveH = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        MoveV = Input.GetAxisRaw("Vertical") * MoveSpeed;
        
        
    }

    void Flip()
    {
        if (transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            transform.eulerAngles = new Vector3(0, 0, 0);
        if (transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            transform.eulerAngles = new Vector3(0, 180, 0);

    }

    public void GetHurtAnim()
    {
        Anim.SetBool("gethurt", true);
    }


    void HurtStop()
    {
        Anim.SetBool("gethurt", false);

    }

}
