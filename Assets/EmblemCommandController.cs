using UnityEngine;
using System.Collections;

public class EmblemCommandController : MonoBehaviour {

    PartsButtonCreator creator = null;

	// Use this for initialization
	void Start () {
        creator = GetComponent<PartsButtonCreator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.P))
        {
            creator.PartsEnable();
        }
	}
}
