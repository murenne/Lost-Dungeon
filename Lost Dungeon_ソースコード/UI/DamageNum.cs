using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNum : MonoBehaviour
{
    public Text DamageText;
    public float LifeTimer;
    public float UpSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, LifeTimer);//ÔÚlifetimerÃëºóÏú»Ù
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, UpSpeed * Time .fixedDeltaTime , 0);
    }

    public void ShowUIDamage(float _amount)
    {
        DamageText.text = _amount.ToString ();
    
    }
}
