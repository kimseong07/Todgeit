using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System.Linq;
using UnityEngine.UI;

public class WebsocketClient : MonoBehaviour
{
    private static WebsocketClient instance;
    public static WebsocketClient GetInstance()
    {
        if (instance == null)
            instance = new WebsocketClient();
        return instance;
    }

    WebSocket ws;
    private readonly ConcurrentQueue<Action> _actions = new ConcurrentQueue<Action>();
    public int clientId = -1;
    public Dictionary<int, Player> playerPool = new Dictionary<int, Player>();

    //server op code
    const int START_COUNTDOWN_OP_CODE = 101;
    const int MOVE_PLAYER_OP_CODE = 102;
    const int JOIN_PLAYER_OP_CODE = 103;

    //client op code
    const int SCENE_READY_OP_CODE = 200;
    const int PING_CHECK_OP_CODE = 205;

    public InputField inputNickName;
    public GameObject playerObj;
    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this);
    }
    void Start()
    {
        //InvokeRepeating("PingCheck", 0, 3f);
    }
    void Update()
    {
        if (ws == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("close");
            ws.Close();
        }
        while (_actions.Count > 0)
        {
            if (_actions.TryDequeue(out var action))
            {
                action?.Invoke();
            }
        }
    }


    public void getStart()
    {
        ws = new WebSocket("ws://127.0.0.1:3003");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Message message = JsonUtility.FromJson<Message>(e.Data);
                switch (message.opCode)
                {
                    case START_COUNTDOWN_OP_CODE:
                        Debug.Log("start");
                        _actions.Enqueue(() => Join(e.Data));
                        break;
                    case JOIN_PLAYER_OP_CODE:
                        Debug.Log("join");
                        _actions.Enqueue(() => GeneratePlayer(e.Data));
                        break;
                    case MOVE_PLAYER_OP_CODE:
                        Debug.Log("move");
                        _actions.Enqueue(() => PlayerUpdate(e.Data));
                        break;
                }

                // _actions.Enqueue(() => PongCheck(e.Data));
            }
            Debug.Log("Message Received From:" + ((WebSocket)sender).Url + ",Data:" + e.Data);
        };
    }

    public void getSend()
    {
        Message msg = new Message();
        msg.opCode = SCENE_READY_OP_CODE;
        msg.nickname = inputNickName.text;
        inputNickName.gameObject.SetActive(false);

        Debug.Log(JsonUtility.ToJson(msg));
        ws.Send(JsonUtility.ToJson(msg));
    }
    public void Join(string data)
    {
        if (clientId >= 0) return;
        Message message = JsonUtility.FromJson<Message>(data);

        switch (message.opCode)
        {
            case START_COUNTDOWN_OP_CODE:
                clientId = message.socketId;
                break;
        }
    }

    public void GeneratePlayer(string data)
    {
        if (clientId < 0) return;
        Message msg = JsonUtility.FromJson<Message>(data);
        Player p = msg.player;
        GameObject o = Instantiate(playerObj, new Vector3(p.posX, p.posY, 0), Quaternion.identity);
        o.GetComponent<Movement>().ownerId = p.owner;
        o.GetComponent<Movement>().nickname.text = p.nickname;

        playerPool.Add(p.owner, p);
        Renderer r = o.GetComponentInChildren<MeshRenderer>();
        r.sharedMaterial = new Material(Shader.Find("Unlit/Color"));
        r.sharedMaterial.SetColor("_Color", new Color(p.r / 255f, p.g / 255f, p.b / 255f));

        Debug.Log("Generate Player");
    }

    public void PlayerUpdate(string data)
    {
        Message msg = JsonUtility.FromJson<Message>(data);
        playerPool[msg.socketId] = msg.player;
        msg.visibleCells.ForEach(cell => {
            playerPool[cell.owner].targetX = cell.targetX;
            playerPool[cell.owner].targetY = cell.targetY;
        });

        Debug.Log("getFromServer:" + msg.player.targetX);
    }

    public void SendUpdate(Vector3 target)
    {
        Message msg = new Message();
        msg.socketId = clientId;
        msg.opCode = MOVE_PLAYER_OP_CODE;
        msg.player = playerPool[clientId];
        msg.player.targetX = target.x;
        msg.player.targetY = target.y;

        ws.Send(JsonUtility.ToJson(msg));
    }

    public DateTime _time;
    public void PingCheck()
    {
        Message msg = new Message();
        msg.opCode = PING_CHECK_OP_CODE;
        msg.socketId = clientId;

        _time = DateTime.Now;
        Debug.Log("time:" + _time.ToString());

        ws.Send(JsonUtility.ToJson(msg));
    }
}
