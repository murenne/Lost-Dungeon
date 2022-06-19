using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    GameObject MapSprite;

    private void OnEnable()
    {
        MapSprite = transform.parent.GetChild(0).gameObject;

        MapSprite.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            MapSprite.SetActive(true);
        }
    }

}
