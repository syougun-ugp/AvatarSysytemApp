using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResetController : MonoBehaviour {

    [SerializeField]
    GameObject avatar = null;

    List<Image> childImage = new List<Image>();

    [SerializeField]
    AccessoryManager accessoryManager = null;
 
	// Use this for initialization
	void Start () {

        foreach(Transform child in avatar.transform)
        {
            if (child.name == "Avatar") continue;

            childImage.Add(child.GetComponent<Image>());
        }

	    GetComponent<Button>().onClick.AddListener(Reset);
	}

    void Reset()
    { 
        foreach(var image in childImage)
        {
            image.enabled = false;
        }

        accessoryManager.AllDestory();
    }


}
