using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 大文字小文字を区別せずに、
/// 文字列内に含まれている数字を考慮して文字列を比較します。
/// </summary>
public class LogicalStringComparer :
    System.Collections.IComparer,
    System.Collections.Generic.IComparer<string>
{
    [System.Runtime.InteropServices.DllImport("shlwapi.dll",
        CharSet = System.Runtime.InteropServices.CharSet.Unicode,
        ExactSpelling = true)]
    private static extern int StrCmpLogicalW(string x, string y);

    public int Compare(string x, string y)
    {
        return StrCmpLogicalW(x, y);
    }

    public int Compare(object x, object y)
    {
        return this.Compare(x.ToString(), y.ToString());
    }
}

public class PartsButtonCreator : MonoBehaviour {

    public enum Type
    {
        Null,
        Eye,
        Eyebrows,
        Group,
        Hair,
        Bottoms,
        Mouth,
        Nose,
        Set,
        Shoes,
        Tops,
        BackGround,
        Accessory,
        Emblem,
    };


    [SerializeField]
    GameObject partsButton = null;

    [SerializeField]
    Image image = null;

    [SerializeField]
    Vector2 intervalPos = new Vector2(110, 110);

    [SerializeField]
    Vector2 createNum = new Vector2(5, 4);

    [SerializeField]
    Type type = Type.Null;

    public Image PartsImage { get { return image; } }

    [SerializeField]
    Button nextButton = null;

    [SerializeField]
    Button backButton = null;

    public Sprite GetSprite(int id)
    {
        return partsSpriteList[id - 1];
    }

    public Sprite GetOriginSprite(int id)
    {
        if (partsSpriteOriginList[id - 1] == null)
        {
            return GetSprite(id);
        }

        return partsSpriteOriginList[id - 1];
    }

    int onePageNum = 0;

    List<Sprite> partsSpriteList = new List<Sprite>();
    List<Sprite> partsSpriteOriginList = new List<Sprite>();

	// Use this for initialization
    void Awake()
    {
        onePageNum = (int)createNum.x * (int)createNum.y;

        var spriteList = Resources.LoadAll<Sprite>(type.ToString());

        Array.Sort(spriteList, new LogicalStringComparer());

        if (type == Type.BackGround || type == Type.Group || type == Type.Emblem)
        {
            for (int i = 0; i < spriteList.Length; i++)
            {
                partsSpriteList.Add(spriteList[i]);
                partsSpriteOriginList.Add(spriteList[i]);
            }
        }
        else
        {
            for (int i = 0; i < spriteList.Length; i += 2)
            {
                partsSpriteList.Add(spriteList[i]);
                partsSpriteOriginList.Add(spriteList[i + 1]);
            }
        }

        enabled = false;

    }

    void Update()
    {

    }

    int index = 1;

    /// <summary>
    /// パーツを有効かにする
    /// それ以外のパーツは有効でなくする。
    /// </summary>
    public void PartsEnable()
    {
        if (enabled) return;

        var partsButtons = FindObjectsOfType<PartsButtonCreator>();
        foreach(var button in partsButtons)
        {
            AllDestory(button.gameObject);
            button.enabled = false;
        }

        enabled = true;

        Create(index);

        nextButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();

        nextButton.onClick.AddListener(Next);
        backButton.onClick.AddListener(Back);
    }

    void Create(int startIndex)
    {
        RectTransform rectTrans = partsButton.transform as RectTransform;

        for (int y = 0; y < createNum.y; y++)
        {
            for (int x = 0; x < createNum.x; x++)
            {
                if (partsSpriteList.Count < startIndex) return;

                var clone = (GameObject)Instantiate(partsButton);
                clone.name = startIndex.ToString();

                clone.GetComponent<AvatarSpritePlacement>().SetImage(this,startIndex,type);

                clone.transform.SetParent(transform);
                clone.transform.localScale = Vector3.one;
                var cloneRectTrans = clone.transform as RectTransform;
                cloneRectTrans.anchoredPosition3D = new Vector3(rectTrans.anchoredPosition.x + (x * intervalPos.x), rectTrans.anchoredPosition.y - (y * intervalPos.y), 0);
                startIndex++;

                if (type == Type.Set || type == Type.BackGround)
                {
                    cloneRectTrans.localScale = new Vector3(
                        cloneRectTrans.localScale.x * 2.0f, cloneRectTrans.localScale.y * 2.4f, 1.0f);
                }
            }
        }

    }

    void AllDestory(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void Next()
    {
        if (index + onePageNum >= partsSpriteList.Count) return;

        AllDestory(gameObject);

        index += onePageNum;
 
        Create(index);
    }

    void Back()
    {
        if (index <= 1) return;

        AllDestory(gameObject);

        index -= onePageNum;

        Create(index);
    }
}
