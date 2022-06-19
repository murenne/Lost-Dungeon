using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManagement : MonoBehaviour
{
    [Header("房间信息")]
    public GameObject RoomPrefab;
    public int RoomNum;
    public Color StartRoomColor, EndRoomColor;
    private GameObject EndRoom;

    [Header("位置控制")]
    public Transform ManagementPoint;
    public float XOffset, YOffset;
    public LayerMask RoomLayer;

    public List<Room> Rooms = new List<Room>();//房间列表,<Room>是每个房间的信息

    public enum DirectionList {Up,Down,Left,Right };//枚举，数值为0~3
    public DirectionList Direction;

    List<GameObject> FurtherRoom = new List<GameObject>();    //最远房间列表
    List<GameObject> LessFurtherRoom = new List<GameObject>();//次远房间列表
    List<GameObject> OneDoorRoom = new List<GameObject>();//只有一个门的房间列表
    public int MaxStepNumble;//最远房间的数字

    public WallType WallTpyes;

    public GameObject[] Emeny;
    public GameObject boss;

    public int EnemyNum ;

    public float RandomSize = 2f;  //+ Random.insideUnitSphere * RandomSize



    // Start is called before the first frame update
    void Awake()
    {

        for (int i = 0; i< RoomNum; i++)
        {
           Rooms.Add (Instantiate(RoomPrefab, ManagementPoint.position, Quaternion.identity).GetComponent<Room > ());//Rooms是房间信息，需要获得component
           Instantiate(Emeny[Random .Range(0,Emeny.Length )], ManagementPoint.position , Quaternion.identity);
            ChangePointPosition();//改变Point的位置
        }

        Rooms[0].GetComponent<SpriteRenderer>().color = StartRoomColor;
        

        EndRoom = Rooms[0].gameObject ;//给最终房间一个初始值
        foreach (var _room in Rooms)//利用中间值来确定最终房间
        {
            //if (_room.transform.position.sqrMagnitude > EndRoom.transform.position.sqrMagnitude)//比较两个房间离原点的距离，取距离较大的为最终房间
            //{
            //    EndRoom = _room.gameObject ;
            //} 

            SetUpRoom(_room ,_room.transform .position );

        }
        FindEndRoom();
        EndRoom.GetComponent<SpriteRenderer>().color = EndRoomColor;

    }

    // Update is called once per frame
    void Update()
    {
        

       // if (Input .GetKeyDown(KeyCode.F ))
       // {
       //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       // }
    }

    void ChangePointPosition()
    {
        do
        {
            
            Direction = (DirectionList)Random.Range(0, 4);//(DirectionList)将枚举类型转换为int类型，0到4不包含4，只取得到0~3的值

            switch (Direction)
            {
                case DirectionList.Up:
                    ManagementPoint.position += new Vector3(0, YOffset, 0);
                    break;

                case DirectionList.Down:
                    ManagementPoint.position += new Vector3(0, -YOffset, 0);
                    break;

                case DirectionList.Left:
                    ManagementPoint.position += new Vector3(-XOffset, 0, 0);
                    break;

                case DirectionList.Right:
                    ManagementPoint.position += new Vector3(XOffset, 0, 0);
                    break;
            }

        } while (Physics2D.OverlapCircle(ManagementPoint.position ,0.2f , RoomLayer ));
    }

    public void SetUpRoom(Room _newroom,Vector3 roomposition)
    {

        _newroom.RoomUp = Physics2D.OverlapCircle(roomposition + new Vector3(0, YOffset, 0), 0.2f, RoomLayer);
        _newroom.RoomDown = Physics2D.OverlapCircle(roomposition + new Vector3(0, -YOffset, 0), 0.2f, RoomLayer);
        _newroom.RoomLeft = Physics2D.OverlapCircle(roomposition + new Vector3(-XOffset, 0, 0), 0.2f, RoomLayer);
        _newroom.RoomRight  = Physics2D.OverlapCircle(roomposition + new Vector3(XOffset, 0, 0), 0.2f, RoomLayer);

        _newroom.UpdataRoom(XOffset,YOffset);

        switch (_newroom.DoorNumber)
        {
            case 1:
                if (_newroom.RoomUp)
                    Instantiate(WallTpyes.WallU,roomposition ,Quaternion.identity);
                if (_newroom.RoomRight)
                    Instantiate(WallTpyes.WallR, roomposition, Quaternion.identity);
                if (_newroom.RoomDown)
                    Instantiate(WallTpyes.WallD, roomposition, Quaternion.identity);
                if (_newroom.RoomLeft)
                    Instantiate(WallTpyes.WallL, roomposition, Quaternion.identity);
             break;

            case 2:
                if (_newroom.RoomUp && _newroom.RoomRight)
                    Instantiate(WallTpyes.WallUR, roomposition, Quaternion.identity);
                if (_newroom.RoomUp && _newroom.RoomDown)
                    Instantiate(WallTpyes.WallUD, roomposition, Quaternion.identity);
                if (_newroom.RoomUp && _newroom.RoomLeft)
                    Instantiate(WallTpyes.WallUL, roomposition, Quaternion.identity);
                if (_newroom.RoomRight && _newroom.RoomDown)
                    Instantiate(WallTpyes.WallRD, roomposition, Quaternion.identity);
                if (_newroom.RoomRight && _newroom.RoomLeft)
                    Instantiate(WallTpyes.WallRL, roomposition, Quaternion.identity);
                if (_newroom.RoomDown && _newroom.RoomLeft)
                    Instantiate(WallTpyes.WallDL, roomposition, Quaternion.identity);
            break;

            case 3:
                if (_newroom.RoomUp && _newroom.RoomRight && _newroom.RoomDown)
                    Instantiate(WallTpyes.WallURD, roomposition, Quaternion.identity);
                if(_newroom.RoomUp && _newroom.RoomRight && _newroom.RoomLeft)
                    Instantiate(WallTpyes.WallURL, roomposition, Quaternion.identity);
                if (_newroom.RoomRight && _newroom.RoomDown && _newroom.RoomLeft)
                    Instantiate(WallTpyes.WallRDL, roomposition, Quaternion.identity);
                if (_newroom.RoomDown && _newroom.RoomLeft && _newroom.RoomUp)
                    Instantiate(WallTpyes.WallDLU, roomposition, Quaternion.identity);
            break;

            case 4:
                if (_newroom.RoomUp && _newroom.RoomRight && _newroom.RoomDown && _newroom.RoomLeft)
                    Instantiate(WallTpyes.WallURDL, roomposition, Quaternion.identity);
            break;



        }



    }

    public void FindEndRoom()
    {
        for (int i = 0; i < Rooms.Count; i++)
        {
            if (Rooms[i].StepToStart > MaxStepNumble)
                MaxStepNumble = Rooms[i].StepToStart;  //获得最远房间的数字
        
        }

        foreach (var _room in Rooms)
        {
            if (_room.StepToStart == MaxStepNumble)
                FurtherRoom.Add(_room.gameObject);   //把最远距离的房间放进列表

            if (_room.StepToStart == MaxStepNumble-1)
                LessFurtherRoom.Add(_room.gameObject);//把次远距离的房间放进列表
        }

        for (int i = 0; i <FurtherRoom .Count  ; i++)   //最远房间里只有一个门的房间添加进来
        {
            if (FurtherRoom[i].GetComponent<Room>().DoorNumber == 1)
                OneDoorRoom.Add(FurtherRoom[i]);           
        }

        for (int i = 0; i < FurtherRoom.Count; i++)   //次远房间里只有一个门的房间添加进来
        {           
            if (LessFurtherRoom[i].GetComponent<Room>().DoorNumber == 1)
                OneDoorRoom.Add(LessFurtherRoom[i]);
        }

        if (OneDoorRoom.Count != 0)
        {
            EndRoom = OneDoorRoom[Random.Range(0, OneDoorRoom.Count)];
            Instantiate(boss, EndRoom.transform.position, Quaternion.identity);
        }
        else
        {
            EndRoom = FurtherRoom[Random.Range(0, FurtherRoom.Count)];
            Instantiate(boss, EndRoom.transform.position, Quaternion.identity);
        }
    }


}

[System .Serializable]
public class WallType
{
    public GameObject WallU, WallR, WallD, WallL,
                       WallUR, WallUD, WallUL, WallRD, WallRL, WallDL,
                       WallURD, WallURL, WallRDL, WallDLU,
                       WallURDL;


}
