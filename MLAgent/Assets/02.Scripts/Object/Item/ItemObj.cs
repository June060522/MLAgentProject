using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : MonoBehaviour,IPoppingObj
{
    [SerializeField]ItemSO itemSO;

    public void PoppingObj()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().stat.isBarrior = itemSO.addBarriorAbility;

            other.GetComponent<Player>().stat.maxBomb += itemSO.addBomb;

            other.GetComponent<Player>().stat.bombPower += itemSO.addbombPower;

            other.GetComponent<Player>().stat.speed += itemSO.addSpeed;
        }
    }
}
