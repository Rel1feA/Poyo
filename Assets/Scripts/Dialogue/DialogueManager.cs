using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextAsset textAsset;//文本文件资源，csv格式

    public SpriteRenderer leftSprite;
    public SpriteRenderer rightSprite;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public List<Sprite> characterImgList;//角色图片列表
    Dictionary<string, Sprite> characterImgDic=new Dictionary<string, Sprite>();//角色图片字典

    public int dialogueIndex;//下一个对话索引值
    public string[] dialogueRows;

    public GameObject optionPrefab;//选项预制体
    public Transform optionRootTra;//选项的根节点

    public bool canClickNext;

    private void Awake()
    {
        characterImgDic["Agnes"] = characterImgList[0];
        characterImgDic["Rel1fe"] = characterImgList[1];
        canClickNext = true;
    }

    private void Start()
    {
        ReadText(textAsset);
        ShowDialogue();
    }

    private void Update()
    {
        if(canClickNext)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ShowDialogue();
            }
        }
    }

    public void UpdateText(string _name,string _dialogue)
    {
        nameText.text = _name;
        dialogueText.text = _dialogue;
    }

    public void UpdateImg(string _name,string pos)
    {
        if(pos=="left")
        {
            leftSprite.sprite = characterImgDic[_name];
        }
        else
        {
            rightSprite.sprite = characterImgDic[_name];
        }
    }

    public void ReadText(TextAsset textAsset)
    {
        dialogueRows = textAsset.text.Split('\n');
    }

    public void ShowDialogue()
    {
        for(int i=0; i < dialogueRows.Length;i++)
        {
            string[] cells = dialogueRows[i].Split(',');
            //这里的#代表execl表中判断对话类型的符号，可自行更改
            //cell里面的索引值要根据设计的对话execl表的列序号来更改,比如下列例子就是第一列是对话索引值，第二列是对话种类
            if (cells[1] == "#" && int.Parse(cells[0]) == dialogueIndex)
            {
                UpdateText(cells[2], cells[3]);
                UpdateImg(cells[2], cells[4]);
                dialogueIndex = int.Parse(cells[5]);
                break;
            }
            else if (cells[1] == "C" && int.Parse(cells[0]) == dialogueIndex)
            {
                canClickNext = false;
                //写执行显示选项的逻辑
                do
                {
                    GenerateOption(dialogueRows[i].Split(','));
                    i++;
                } while (dialogueRows[i].Split(',')[1]=="C");
                break;
            }
            else if (cells[1]=="END"&& int.Parse(cells[0]) == dialogueIndex)
            {
                //结束对话
                break;
            }
        }
    }

    public void GenerateOption(string[] optionData)
    {
        GameObject option=Instantiate(optionPrefab,optionRootTra);
        option.GetComponent<Button>().onClick.AddListener(delegate
        {
            dialogueIndex = int.Parse(optionData[5]);
            ShowDialogue();
            //删除选项按钮
            for(int i = 0;i<optionRootTra.childCount;i++)
            {
                Destroy(optionRootTra.GetChild(i).gameObject);
            }
        });
        option.GetComponentInChildren<TMP_Text>().text = optionData[3];
    }
}
