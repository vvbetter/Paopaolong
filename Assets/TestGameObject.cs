using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameObject : MonoBehaviour {

    private GameObject netManager;
	// Use this for initialization
	void Start () {
        netManager = GameObject.Find("/NetManager");
        if (netManager == null)
        {
            Debug.Log("没有找到NetManager对象");
        }
        else
        {
            Debug.Log("找到了NetManager");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
