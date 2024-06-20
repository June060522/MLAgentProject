using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Dictionary<IPoppingObj, Vector2Int> lists;
    RealPlayer[] testPlayers;
    Player[] players;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        players = FindObjectsOfType<Player>();
        testPlayers = FindObjectsOfType<RealPlayer>();
    }


    private void FixedUpdate()
    {
        lists = new();

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
            {
                lists.Add(players[i], players[i].GetPositionIndex());
            }
        }

        for (int i = 0; i < testPlayers.Length; i++)
        {
            if (testPlayers[i] != null)
            {
                lists.Add(testPlayers[i], testPlayers[i].GetPositionIndex() + new Vector2Int(7,6));
            }
        }
    }

    public void IsBomb(Vector2Int pos)
    {
        foreach (var player in lists)
        {
            if (player.Value == pos)
                player.Key.PoppingObj();
        }
    }
}