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
    public static Game Instance;

    HashSet<Player> players = new HashSet<Player>();

    [SerializeField] PlayerSpot[] playerList;
    [SerializeField] BrokenBlockSpot[] brokenBlockList;
    [SerializeField] ObstacleBlockSpot[] obstacleBlockList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError($"{transform} : Game is multiply running!");
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for(int i = 0 ; i < playerList.Length; i++)
        {
            GameObject player = Instantiate(playerList[i].player, PositionManager.Instance.GetWorldPosition(playerList[i].playerSpot),Quaternion.identity);
        }

        for (int i = 0; i < brokenBlockList.Length; i++)
        {
            Vector3 worldPos = PositionManager.Instance.GetWorldPosition(brokenBlockList[i].brokenBlockSpot,-8f);

            Vector2Int pos = brokenBlockList[i].brokenBlockSpot;
            pos.x += 7;
            pos.y += 7;

            BrokenObject brokenObj = Instantiate(brokenBlockList[i].brokenBlockObj, worldPos, Quaternion.identity);

            MapManager.Instance.SetTileType(pos, TileType.BrokenWall);

            MapManager.Instance.SetPoppingObj(pos, brokenObj);
        }

        for (int i = 0; i < obstacleBlockList.Length; i++)
        {
            Vector3 worldPos = PositionManager.Instance.GetWorldPosition(obstacleBlockList[i].obstacleBlockSpot,-8f);


            GameObject obstacleBlock = Instantiate(obstacleBlockList[i].obstacleBlockObj, worldPos, Quaternion.identity);
        }
    }
}