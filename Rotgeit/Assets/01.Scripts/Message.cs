using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
    public int socketId;
    public int opCode;
    public Player player;
    public List<Player> visibleCells;
    public string nickname;
}
