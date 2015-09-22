using UnityEngine;
using System.Collections;

public class ImageWaver : MonoBehaviour {

    [SerializeField]
    float upPosY = 100;

    [SerializeField]
    float time = 1;

    [SerializeField]
    float delayTime = 0;

    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.linear;


    RectTransform rectTrans = null;

	// Use this for initialization
	void Start () {
        rectTrans = transform as RectTransform;
        float y = rectTrans.anchoredPosition.y;
        iTween.ValueTo(gameObject, iTween.Hash("from", y, "to", y + upPosY, "time", time,
            "looptype", iTween.LoopType.pingPong,"delay",delayTime, "easetype", easeType, "onupdate", "UpdateHandler"));
    }

    void UpdateHandler(float value)
    {
        rectTrans.anchoredPosition3D = new Vector3(rectTrans.anchoredPosition.x, value, 0);
    }


}
