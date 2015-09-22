using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AccessoryManager : MonoBehaviour {

    List<Image> accessoryList = new List<Image>();

    public void AllDestory()
    {
        foreach (var list in accessoryList)
        {
            Destroy(list.gameObject);
        }

        accessoryList.Clear();
    }

    public void OnClick(Image image ,Sprite sprite)
    {
        var accessory = accessoryList.Find(i => i.sprite.name == sprite.name);

        if (accessory == null)
        {
            AddInstantiate(image, sprite);
        }
        else
        {
            Remove(accessory);
        }

    }

    void Remove(Image data)
    {
        accessoryList.Remove(data);
        Destroy(data.gameObject);
    }

    void AddInstantiate(Image image ,Sprite sprite)
    {
        var clone = Instantiate(image.gameObject);
        clone.transform.SetParent(image.transform.parent);
        clone.name = image.name;
        var rectTrans = clone.transform as RectTransform;
        rectTrans.localScale = Vector3.one;
        rectTrans.anchoredPosition3D = Vector3.zero;
        var cloneImage = clone.GetComponent<Image>();
        cloneImage.sprite = sprite;
        cloneImage.enabled = true;
        accessoryList.Add(cloneImage);
    }
}
