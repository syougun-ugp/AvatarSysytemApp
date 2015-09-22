using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AvatarSpritePlacement : MonoBehaviour {

    Image image = null;
    Image myImage = null;

    PartsButtonCreator partsCreator = null;
    PartsButtonCreator.Type type = PartsButtonCreator.Type.Null;

    AccessoryManager accessoryManager = null;

    void Start()
    {
        image = partsCreator.PartsImage;
        GetComponent<Button>().onClick.AddListener(Placement);

	}


    public void SetImage(PartsButtonCreator creator ,int id,PartsButtonCreator.Type type)
    {
        partsCreator = creator;
        myImage = transform.GetChild(0).GetComponent<Image>();
        myImage.sprite = partsCreator.GetSprite(id);
        this.type = type;
    }

    /// <summary>
    /// 配置する
    /// </summary>
    void Placement()
    {
        if (type == PartsButtonCreator.Type.Accessory)
        {
            if (accessoryManager == null)
            {
                accessoryManager = partsCreator.GetComponent<AccessoryManager>();
            }

            accessoryManager.OnClick(image, partsCreator.GetOriginSprite(int.Parse(name)));
        }
        else
        {
            image.enabled = true;
            image.sprite = partsCreator.GetOriginSprite(int.Parse(name));
        }
    }
}
