using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bomb : MonoBehaviour, IPoppingObj
{
    public Transform trm;
    PlayerStat _modifierStat;
    Vector2Int _pos;
    Player player;
    private WaitForSeconds wfs = new WaitForSeconds(2.5f);
    Vector2Int pos = Vector2Int.zero;
    [SerializeField] GameObject particle;
    private void Start()
    {
        trm = GetComponent<Transform>();
        player = transform.parent.parent.parent.GetComponentInChildren<Player>();
        pos = player.positionManager.GetPositionIndex(transform.localPosition) + new Vector2Int(7, 6);
        
        if (pos.x < 0 || pos.y < 0 || pos.x > 15 || pos.y > 13 || player.mapManager.GetPoppingObj(pos) != null)
        {
            _modifierStat.useBomb--;
            Destroy(gameObject);
            return;
        }
        StartCoroutine(IExplore());
        player.mapManager.SetPoppingObj(pos, this);
        player.mapManager._tileTypes[pos.x,pos.y] = true;
    }

    IEnumerator IExplore()
    {
        yield return wfs;
        Explore();
    }

    public void Explore()
    {
        player.mapManager._tileTypes[pos.x, pos.y] = false;
        StopAllCoroutines();
        player.mapManager.SetPoppingNull(transform.localPosition);
        Popping();
        _modifierStat.useBomb--;
        Destroy(gameObject);
    }

    private void Popping()
    {
        for (int i = 1; i <= _modifierStat.bombPower; ++i)
        {
            Vector2Int pos = _pos + new Vector2Int(i, 0);

            //예외처리
            if (pos.x < 0 || pos.y < 0 || pos.x > 15 || pos.y > 13)
                continue;

            player.playerManager.IsBomb(pos);
            IPoppingObj obj = player.mapManager.GetPoppingObj(pos);
            Instantiate(particle, player.positionManager.GetWorldPosition(pos - new Vector2Int(7,6)),Quaternion.identity);
            if (obj != null)
            {
                obj.PoppingObj();
                break;
            }
        }

        for (int i = -1; i >= -_modifierStat.bombPower; --i)
        {
            Vector2Int pos = _pos + new Vector2Int(i, 0);

            //예외처리
            if (pos.x < 0 || pos.y < 0 || pos.x > 15 || pos.y > 13)
                continue;

            player.playerManager.IsBomb(pos);
            IPoppingObj obj = player.mapManager.GetPoppingObj(pos);
            Instantiate(particle, player.positionManager.GetWorldPosition(pos - new Vector2Int(7, 6)), Quaternion.identity);
            if (obj != null)
            {
                obj.PoppingObj();
                break;
            }
        }

        for (int i = 1; i <= _modifierStat.bombPower; ++i)
        {
            Vector2Int pos = _pos + new Vector2Int(0, i);

            //예외처리
            if (pos.x < 0 || pos.y < 0 || pos.x > 15 || pos.y > 13)
                continue;

            player.playerManager.IsBomb(pos);
            IPoppingObj obj = player.mapManager.GetPoppingObj(pos);
            Instantiate(particle, player.positionManager.GetWorldPosition(pos - new Vector2Int(7, 6)), Quaternion.identity);
            if (obj != null)
            {
                obj.PoppingObj();
                break;
            }
        }

        for (int i = 0; i >= -_modifierStat.bombPower; --i)
        {
            Vector2Int pos = _pos + new Vector2Int(0, i);

            //예외처리
            if (pos.x < 0 || pos.y < 0 || pos.x > 15 || pos.y > 13)
                continue;

            player.playerManager.IsBomb(pos);
            IPoppingObj obj = player.mapManager.GetPoppingObj(pos);
            Instantiate(particle, player.positionManager.GetWorldPosition(pos - new Vector2Int(7, 6)), Quaternion.identity);
            if (obj != null)
            {
                obj.PoppingObj();
                break;
            }
        }
    }

    public void SetStat(PlayerStat stat, Vector2Int pos)
    {
        _modifierStat = stat;
        _pos = pos;
    }

    void IPoppingObj.PoppingObj()
    {
        Explore();
    }
}
