using UnityEngine;
using System.Collections;

public class CircleRotater : MonoBehaviour {

    [SerializeField]
    float time = 1;

    [SerializeField]
    float delayTime = 0;

    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.linear;

    RectTransform rectTrans = null;

    float updateTime = 0;

    // Use this for initialization
    void Start()
    {
        updateTime = delayTime;
        rectTrans = transform as RectTransform;
    }

    void Update()
    {
        updateTime -= Time.deltaTime;
        if (updateTime <= 0.0f)
        {
            updateTime = time;
            iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 360, "time", time, "easetype", easeType, "onupdate", "UpdateHandler"));
        }
    }

    void UpdateHandler(float value)
    {
        rectTrans.rotation = Quaternion.Euler(new Vector3(0, 0, -value));
    }
}
