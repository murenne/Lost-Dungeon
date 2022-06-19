using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : MonoBehaviour
{

    public GameObject HPBottleParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(HPBottleParticle, transform.position, Quaternion.identity);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>().PlayerCurrentHP += 5;
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
