using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour, IPoppingObj
{
    Player player;
    private void Start()
    {
        player = transform.parent.parent.parent.GetComponentInChildren<Player>();
        player.mapManager.SetPoppingObj(player.positionManager.GetPositionIndex(transform.localPosition) + new Vector2Int(7, 6), this);
    }

    public void PoppingObj()
    {

    }
}
