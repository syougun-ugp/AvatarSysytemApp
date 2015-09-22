using UnityEngine;
using System.Collections;

public class LoadingManager : MonoBehaviour {

    [SerializeField]
    float time = 1.0f;

    AsyncOperation async = null;

	// Use this for initialization
	void Start () {
        async = Application.LoadLevelAsync("main");
        async.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;

        if (async.progress >= 0.9f && time <= 0)
        {
            async.allowSceneActivation = true;
        }
	}
}
