using UnityEngine;

public class MapManager : MonoBehaviour
{
    public bool[,] _tileTypes = new bool[16,14];

    private IPoppingObj[,] _poppingObjs = new IPoppingObj[16,14];
    private IPoppingObj[,] poppingObjs => _poppingObjs;

    Player player;

    private void Awake()
    {
        for (int i = 1; i < 16; i++)
        {
            for(int j = 1;  j < 14; j++)
            {
                _tileTypes[i, j] = false;
            }
        }
    }

    private void Start()
    {
        player = transform.parent.parent.GetComponentInChildren<Player>();
    }

    public void Init()
    {
        _tileTypes = new bool[16, 14];
        _poppingObjs = new IPoppingObj[16, 14];
        for (int i = 1; i < 16; i++)
        {
            for (int j = 1; j < 14; j++)
            {
                _tileTypes[i, j] = false;
            }
        }
    }

    public void SetTileType(Vector2Int pos, bool tileType)
        => _tileTypes[pos.x,pos.y] = tileType;

    public bool GetTileType(Vector2Int pos) => _tileTypes[pos.x, pos.y];

    public bool[,] GetTileTypes() => _tileTypes;

    public void SetPoppingObj(Vector2Int pos, IPoppingObj obj)
        => _poppingObjs[pos.x, pos.y] = obj;

    public void SetPoppingNull(Vector3 position) => SetPoppingObj(player.positionManager.GetPositionIndex(position) + new Vector2Int(7, 6), null);

    public IPoppingObj GetPoppingObj(Vector2Int pos) => _poppingObjs[pos.x, pos.y];

    public IPoppingObj[,] GetPoppingObjs() => _poppingObjs;
}
