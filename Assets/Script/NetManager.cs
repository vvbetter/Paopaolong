using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJson;
using Pomelo.DotNetClient;
public class NetManager : MonoBehaviour
{
    public delegate void CbFunc(JsonObject rst);
    // Use this for initialization
    public string host;
    public ushort port;
    public string router;
    PomeloClient client = null;
    Dictionary<string, CbFunc> callbacks = null;

    void Awake()
    {
        callbacks = new Dictionary<string, CbFunc>();
        //初始化 PomeloClient
        client = new PomeloClient();
        client.initClient(host, port, () => {
            client.connect(null, (data) => {
                Debug.Log("connect back data" + data.ToString());
                JsonObject js = new JsonObject();
                js["uid"] = 12234;
                client.request(router, js, (connectData) =>
                {
                    string conHost = connectData["host"].ToString();
                    int conPort = int.Parse(connectData["port"].ToString());
                    Debug.Log(conHost + "#" + conPort.ToString());
                    client.disconnect();

                    client = new PomeloClient();
                    client.initClient(conHost, conPort, () =>
                    {
                        client.connect(null, (cb) => {
                        });
                    });
                });
            });
        });
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public bool RegRouterListener(string router, CbFunc cb)
    {
        if( callbacks.ContainsKey(router))
        {
            return false;
        }
        callbacks.Add(router, cb);
        client.on(router, (rst) =>
        {
            cb(rst);
        });
        return true;
    }

    public void Request(string router,JsonObject msg,CbFunc cb)
    {
        client.request(router, msg, (rst) => {
            cb(rst);            
        });
    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnDestroy()
    {
        client.disconnect();
    }

    void Enter()
    {
        JsonObject js = new JsonObject();
        js["username"] = 12234;
        js["rid"] = "aaaa";
        string route = "connector.entryHandler.enter";
        client.request(route, js, Test); 
    }

    public void Test(JsonObject rst)
    {
        Debug.Log("Enter response data:" + rst.ToString());
        JsonArray users = (JsonArray)rst["users"];
        
        for( int i = 0;i<users.Count; ++i)
        {
            Debug.Log(users[i].ToString());
        }
    }
    
}
