using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJson;

public class Connector : MonoBehaviour {

    NetManager netManager = null;
	// Use this for initialization
	void Start () {
        netManager = GameObject.Find("/NetManager").GetComponent<NetManager>();
        if(netManager == null)
        {
            Debug.Log("Connetro 没有找到 NetManager");
        }

        netManager.RegRouterListener("onChat", OnChat);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("enter");
            JsonObject js = new JsonObject();
            js["username"] = 12234;
            js["rid"] = "aaaa";
            string route = "connector.entryHandler.enter";
            netManager.Request(route, js, (JsonObject rst)=> {
                Debug.Log("Enter response data:" + rst.ToString());
                JsonArray users = (JsonArray)rst["users"];

                for (int i = 0; i < users.Count; ++i)
                {
                    Debug.Log(users[i].ToString());
                }
            });
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            SendChat();
        }
    }
    void SendChat()
    {
        string router = "chat.chatHandler.send";
        JsonObject js = new JsonObject();
        js["rid"] = "aaaa";
        js["content"] = "hello world";
        js["from"] = 12234;
        js["target"] = "*";
        netManager.Request(router, js, (JsonObject rst)=> {
            Debug.Log("SendChatBack" + rst.ToString());
        });
    }
    void OnChat(JsonObject rst)
    {
        Debug.Log("onchat " + rst.ToString());
    }
}
