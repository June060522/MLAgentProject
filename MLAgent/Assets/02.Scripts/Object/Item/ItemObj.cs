using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : MonoBehaviour,IPoppingObj
{
    [SerializeField]ItemSO itemSO;
    Player player;

    public void PoppingObj()
    {
        player.mapManager.SetPoppingNull(transform.localPosition);
        Destroy(gameObject);
    }

    private void Awake()
    {
        if(itemSO == null)
        {
            Debug.LogError($"{transform.name} : itemSO is null!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = transform.parent.parent.parent.GetComponentInChildren<Player>();
        player.mapManager.SetPoppingObj(player.positionManager.GetPositionIndex(transform.localPosition) + new Vector2Int(7, 6), this);

        if(transform.name != "Shield(Clone)")
        transform.localRotation = Quaternion.Euler(-90,0,0);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Ghost"))
        {
            if (other.GetComponent<Player>() != null && itemSO != null)
            {

                other.GetComponent<Player>().stat.isBarrior |= itemSO.addBarriorAbility;

                other.GetComponent<Player>().stat.maxBomb += itemSO.addBomb;

                other.GetComponent<Player>().stat.bombPower += itemSO.addbombPower;

                other.GetComponent<Player>().stat.speed += itemSO.addSpeed;

            }    


            player.mapManager.SetPoppingNull(transform.localPosition);
            Destroy(gameObject);
        }
    }
}
