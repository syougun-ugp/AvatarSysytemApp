using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AvatarBodyColorChanger : MonoBehaviour {

    [SerializeField]
    Sprite bodyColorSprite = null;

    [SerializeField]
    Image avatarBody = null;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Change);
    }

    public void Change()
    {
        avatarBody.sprite = bodyColorSprite;
    }
}
