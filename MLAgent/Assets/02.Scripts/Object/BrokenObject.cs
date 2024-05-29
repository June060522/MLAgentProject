using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObject : MonoBehaviour, IPoppingObj
{
    [SerializeField] private int hp = 1;

    public void PoppingObj()
    {
        hp--;
        if (hp <= 0)
            Broken();
    }

    private void Broken()
    {
        int percent = Random.Range(0, 10);

        if (percent < 6)
        {
            ItemManager.Instance.CreateItem(transform.position);
        }
        Destroy(gameObject);
    }
}
