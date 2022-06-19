using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayer : MonoBehaviour
{

    private Transform TS;

    // Start is called before the first frame update
    void Start()
    {
        TS = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        TS.position  = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
