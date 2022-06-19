using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject DoorLeft, DoorRight, DoorUp, DoorDown;

    public bool RoomUp, RoomDown, RoomLeft, RoomRight;//判断哪个方向有房间

    public int StepToStart;

    public Text text;

    public int DoorNumber;

    RoomManagement RM;



    // Start is called before the first frame update
    void Start()
    {
        RM = GameObject.Find("RoomManagement").GetComponent<RoomManagement>();
    }

    public void UpdataRoom(float _xoffset,float _yoffset)
    {
        StepToStart = (int )(Mathf.Abs(transform.position.x / _xoffset) + Mathf.Abs(transform.position.y / _yoffset));

        text.text = StepToStart.ToString();

        if (RoomUp)
            DoorNumber++;
        if (RoomDown)
            DoorNumber++;
        if (RoomLeft )
            DoorNumber++;
        if (RoomRight )
            DoorNumber++;
    }

    private void FixedUpdate()
    {
        if (RM.EnemyNum > 0)
        {
            DoorClose();
        }
        else
        {
            DoorOpen();
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.tag == "Player")
        {
            CameraControl.Instance.ChangeTarget(transform );
        }
      
    }



    void DoorClose()
    {
            
            DoorLeft.SetActive(RoomLeft);
            DoorUp.SetActive(RoomUp);
            DoorRight.SetActive(RoomRight);
            DoorDown.SetActive(RoomDown);            
    }

    void DoorOpen()
    {
            DoorLeft.SetActive(false);
            DoorUp.SetActive(false);
            DoorRight.SetActive(false);
            DoorDown.SetActive(false);
    }


    private void Update()
    {





    }

}
