using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerSpot
{
    public Vector2Int playerSpot;
    public GameObject player;
}

[System.Serializable]
public struct BrokenBlockSpot
{
    public Vector2Int brokenBlockSpot;
    public BrokenObject brokenBlockObj;
}

[System.Serializable]
public struct ObstacleBlockSpot
{
    public Vector2Int obstacleBlockSpot;
    public GameObject obstacleBlockObj;
}

public class Game : MonoBehaviour
{
    HashSet<Player> players = new HashSet<Player>();

    [SerializeField] PlayerSpot[] playerList;
    [SerializeField] BrokenBlockSpot[] brokenBlockList;
    [SerializeField] ObstacleBlockSpot[] obstacleBlockList;
    public Transform parent;
    Player player;
    Transform parentPos;

    private void Start()
    {
        player = transform.parent.parent.GetComponentInChildren<Player>();
        parentPos = transform.parent.parent.parent;
    }

    public void Init()
    {
        if (player == null)
            player = transform.parent.parent.GetComponentInChildren<Player>();

        if(parentPos == null)
            parentPos = parent.parent.parent;
        Debug.Log(parentPos);
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
        //
        for (int i = 0; i < LevelManager.instance.level + 2; i++)
        {
            Instantiate(playerList[Random.Range(0,3)].player, player.positionManager.GetWorldPosition(playerList[Random.Range(0, 3)].playerSpot) + parentPos.position, Quaternion.identity, parent);
        }
        for (int i = 0; i < brokenBlockList.Length; i++)
        {
            Vector3 worldPos = player.positionManager.GetWorldPosition(brokenBlockList[i].brokenBlockSpot, -8f);

            Vector2Int pos = brokenBlockList[i].brokenBlockSpot;
            pos.x += 7;
            pos.y += 6;
            BrokenObject brokenObj = Instantiate(brokenBlockList[i].brokenBlockObj, worldPos + parentPos.position, Quaternion.identity, parent);

            player.mapManager.SetPoppingObj(pos, brokenObj);
        }

        //for (int i = 0; i < obstacleBlockList.Length; i++)
        //{
        //Vector3 worldPos = player.positionManager.GetWorldPosition(obstacleBlockList[i].obstacleBlockSpot,-8f);


        //GameObject obstacleBlock = Instantiate(obstacleBlockList[i].obstacleBlockObj, worldPos + parentPos.position, Quaternion.identity, parent);
        //}
    }
}