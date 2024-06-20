using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    [SerializeField] Bomb bomb;

    Player player;

    private void Start()
    {
        player = transform.parent.parent.GetComponentInChildren<Player>();
    }

    public void OnUseBomb(Vector3 position,PlayerStat stat)
    {
        position.x -= transform.parent.parent.position.x;
        position.z -= transform.parent.parent.position.z;
        Vector2Int worldIndex = player.positionManager.GetPositionIndex(position);
        Vector3 worldPos = player.positionManager.GetWorldPosition(worldIndex);
        worldIndex += new Vector2Int(7, 6);
        Bomb bombObj = Instantiate(bomb, worldPos + transform.parent.parent.position, Quaternion.identity, player.game.parent);
        bombObj.SetStat(stat,worldIndex);
    }
}
