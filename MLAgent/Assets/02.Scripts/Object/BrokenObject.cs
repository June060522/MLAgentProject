using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObject : MonoBehaviour, IPoppingObj
{
    [SerializeField] private int hp = 1;

    Player player;

    private void Start()
    {
        player = transform.parent.parent.parent.GetComponentInChildren<Player>();
    }

    public void PoppingObj()
    {
        hp--;
        if (hp <= 0)
            Broken();
    }

    private void Broken()
    {
        if(player == null)
            player = transform.parent.parent.parent.GetComponentInChildren<Player>();
        player.mapManager.SetPoppingNull(transform.localPosition);

        int percent = Random.Range(0, 10);

        if (percent < 6)
        {
            player.itemManager.CreateItem(transform.position);
        }


        Destroy(gameObject);
    }
}
