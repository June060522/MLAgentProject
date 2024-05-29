using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IPoppingObj
{
    PlayerStat _modifierStat;
    Vector2Int _pos;

    private WaitForSeconds wfs = new WaitForSeconds(1f);
    
    private void Start()
    {
        StartCoroutine(IExplore());
    }

    IEnumerator IExplore()
    {
        yield return wfs;
        Explore();
    }

    public void Explore()
    {
        StopAllCoroutines();
        Popping();
        _modifierStat.useBomb--;
        Destroy(gameObject);
    }

    private void Popping()
    {
        //for(int i = )
    }

    public void SetStat(PlayerStat stat,Vector2Int pos)
    {
        _modifierStat = stat;
        _pos = pos;
    }
    void IPoppingObj.PoppingObj()
    {
        Explore();
    }
}
