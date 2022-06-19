using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header ("UI组件")]
    public Text TextLabel;
    public Image FaceImage;



    [Header("文本文件")]
    public TextAsset TextFile;
    public int Index;
    public float TextSpeed;



    [Header("头像")]
    public Sprite Face01;
    public Sprite Face02;

    List<string> TextList = new List<string>();

    bool TextFinish;    //是否完成打字
    bool CancelType;   //取消打字


    // Start is called before the first frame update
    void Awake()                       //把start换成awake,目的是在onenable之前进行调用
    {
        GetTextFromFile(TextFile );     //游戏开始就从textfile读取文档
        
    }

    private void OnEnable()      //直接输出第一行文本
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
            if (TextFinish && !CancelType)  //已经输出结束，并且没有取消一个字一个字输出
            {
                StartCoroutine(SetTextUI());
            }
            else if (!TextFinish && !CancelType) //正在一个字一个字输出，并且没有取消一个字一个字输出
            {
                // CancelType = !CancelType;  // 按一下就改成true
                CancelType = true;            //变成true就取消了一个字一个字输出


            }
        
        }



    }

    void GetTextFromFile(TextAsset  file)         //将文本里的文字变成字符型并分割
    {
        TextList.Clear();   //清空列表，以免每次运行会生成之前的文本信息
        Index = 0;          //配套设施

         var LineData =  file.text.Split('\n');    //按行切割，定义临时变量linedata，var会自动识别这个变量的类型
       

        foreach (var Line in LineData)
        {
            TextList.Add(Line);           //将切割的每一行加到textlist
        }
    
    }

    IEnumerator SetTextUI()
    {
        TextFinish = false;
        TextLabel.text = "";

        switch (TextList[Index].Trim())  //TextList[Index]是这一行的内容
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
        while (!CancelType && Letter < TextList[Index].Length - 1) //没有再次按下就执行，按下就跳过
        {
            TextLabel.text += TextList[Index][Letter];
            Letter++;
            yield return new WaitForSeconds(TextSpeed);


        }

        TextLabel.text = TextList[Index];               //CancelType=true时，即按下r就执行就执行

        CancelType = false;                            //之前变成了true，现在要改成false
        TextFinish = true;
        Index++;
    
    }
}
