using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private WaitForSeconds wfs = new WaitForSeconds(1f);
    private void Awake()
    {
        StartCoroutine(IExplore());
    }

    IEnumerator IExplore()
    {
        yield return wfs;
    }

    public void Explore()
    {
        StopAllCoroutines();
        Debug.Log("펑");
    }
}
