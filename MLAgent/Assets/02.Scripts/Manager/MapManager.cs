using UnityEngine;

public enum TileType
{
    Empty, // 비어있을때
    
    Bomb, // 폭탄이 있을때
    Item, // 아이템이 있을때
    Warning, // 곧 터질 구역
    Wall, // 벽
    BrokenWall, // 부숴지는 벽
    Player, // 플레이어가 있음

    WarningPlayer, // 위험지역에 플레이어가 있음
    WarningItem, // 위험지역에 아이템이 있음
    WarningBrokenWall, // 위험지역에 부서지는 벽이 있음
}

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    private TileType[,] _tileTypes = new TileType[16,16];
    public TileType[,] tileTypes => _tileTypes;

    private IPoppingObj[,] poppingObjs = new IPoppingObj[16,16];

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError($"{transform} : MapManager Is Multiply running!");
            Destroy(gameObject);
        }

        for (int i = 1; i < 16; i++)
        {
            for(int j = 1;  j < 16; j++)
            {
                _tileTypes[i, j] = TileType.Empty;
            }
        }
    }

    public void SetTileType(Vector2Int pos, TileType tileType)
        => _tileTypes[pos.x,pos.y] = tileType;

    public TileType GetTileType(Vector2Int pos) => _tileTypes[pos.x, pos.y];

    public TileType[,] GetTileTypes() => tileTypes;

    public void SetPoppingObj(Vector2Int pos, IPoppingObj obj)
        => poppingObjs[pos.x, pos.y] = obj;

    public IPoppingObj GetPoppingObj(Vector2Int pos) => poppingObjs[pos.x, pos.y];

    public IPoppingObj[,] GetPoppingObjs() => poppingObjs;
}
