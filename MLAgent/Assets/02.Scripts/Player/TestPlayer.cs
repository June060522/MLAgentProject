using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour, IPoppingObj
{
    PlayerStat stat;

    Player player;

    private void Start()
    {
        player = transform.parent.parent.parent.GetComponentInChildren<Player>();
    }

    public Vector2Int GetPositionIndex()
    {
        if(player == null)
            player = transform.parent.parent.parent.GetComponentInChildren<Player>();
        return player.positionManager.GetPositionIndex(transform.localPosition);
    }
    public void PoppingObj()
    {
        Destroy(gameObject);
    }
}