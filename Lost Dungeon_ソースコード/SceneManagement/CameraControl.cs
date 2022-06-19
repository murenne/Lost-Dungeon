using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static  CameraControl Instance;//µ¥Àý£¬Òª¸Ä

    public float CameraSpeed;

    public Transform RoomTarget;

    [Header("Camera Shake")]
    private Vector3 shakeActive;
    private float shakeAmplify;

    // Start is called before the first frame update
    private  void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(RoomTarget!=null)
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(RoomTarget.position.x, RoomTarget.position.y, transform.position.z), CameraSpeed*Time.deltaTime);

        if (shakeAmplify > 0)
        {
            shakeActive = new Vector3(Random.Range(-shakeAmplify, shakeAmplify), Random.Range(-shakeAmplify, shakeAmplify), 0f);
            shakeAmplify -= Time.deltaTime;
        }
        else
        {
            shakeActive = Vector3.zero;
        }

        transform.position += shakeActive;
    }

    public void ChangeTarget(Transform _RoomTarget)
    {
        RoomTarget = _RoomTarget;
    
    
    }

    public void CameraShake(float _amount)
    {
        shakeAmplify = _amount;
    }

}

