using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header ("UI���")]
    public Text TextLabel;
    public Image FaceImage;



    [Header("�ı��ļ�")]
    public TextAsset TextFile;
    public int Index;
    public float TextSpeed;



    [Header("ͷ��")]
    public Sprite Face01;
    public Sprite Face02;

    List<string> TextList = new List<string>();

    bool TextFinish;    //�Ƿ���ɴ���
    bool CancelType;   //ȡ������


    // Start is called before the first frame update
    void Awake()                       //��start����awake,Ŀ������onenable֮ǰ���е���
    {
        GetTextFromFile(TextFile );     //��Ϸ��ʼ�ʹ�textfile��ȡ�ĵ�
        
    }

    private void OnEnable()      //ֱ�������һ���ı�
    {
        //TextLabel.text = TextList[Index];   
        //Index++;
        TextFinish = true;
        StartCoroutine(SetTextUI());
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && Index == TextList.Count)
        {
            gameObject.SetActive(false);
            Index = 0;
            return;
        }


        //if (Input.GetKeyDown(KeyCode.R)&&TextFinish )
        //{
        //    //TextLabel.text = TextList[Index ];
        //    //Index++;

        //    StartCoroutine(SetTextUI());
        //}


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (TextFinish && !CancelType)  //�Ѿ��������������û��ȡ��һ����һ�������
            {
                StartCoroutine(SetTextUI());
            }
            else if (!TextFinish && !CancelType) //����һ����һ�������������û��ȡ��һ����һ�������
            {
                // CancelType = !CancelType;  // ��һ�¾͸ĳ�true
                CancelType = true;            //���true��ȡ����һ����һ�������


            }
        
        }



    }

    void GetTextFromFile(TextAsset  file)         //���ı�������ֱ���ַ��Ͳ��ָ�
    {
        TextList.Clear();   //����б�����ÿ�����л�����֮ǰ���ı���Ϣ
        Index = 0;          //������ʩ

         var LineData =  file.text.Split('\n');    //�����и������ʱ����linedata��var���Զ�ʶ���������������
       

        foreach (var Line in LineData)
        {
            TextList.Add(Line);           //���и��ÿһ�мӵ�textlist
        }
    
    }

    IEnumerator SetTextUI()
    {
        TextFinish = false;
        TextLabel.text = "";

        switch (TextList[Index].Trim())  //TextList[Index]����һ�е�����
        {
            case "A":
                FaceImage.sprite = Face01;
                Index++;
                break;

            case "B":
                FaceImage.sprite = Face02;
                Index++;
                break;

        }

        //for (int i = 0; i <TextList[Index].Length  ; i++)
        //{
        //    TextLabel.text += TextList[Index][i];

        //    yield return new WaitForSeconds(TextSpeed);


        //}

        int Letter = 0;
        while (!CancelType && Letter < TextList[Index].Length - 1) //û���ٴΰ��¾�ִ�У����¾�����
        {
            TextLabel.text += TextList[Index][Letter];
            Letter++;
            yield return new WaitForSeconds(TextSpeed);


        }

        TextLabel.text = TextList[Index];               //CancelType=trueʱ��������r��ִ�о�ִ��

        CancelType = false;                            //֮ǰ�����true������Ҫ�ĳ�false
        TextFinish = true;
        Index++;
    
    }
}
