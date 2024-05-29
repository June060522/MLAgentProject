using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public static BombManager Instance;
    [SerializeField] Bomb bomb;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError($"{transform} : BombManager is multiply running!");
        }
    }

    public void OnUseBomb(Vector3 position,PlayerStat stat)
    {
        Vector2Int worldIndex = PositionManager.Instance.GetPositionIndex(position);

        Vector3 worldPos = PositionManager.Instance.GetWorldPosition(worldIndex);

        Bomb bombObj = Instantiate(bomb, worldPos, Quaternion.identity);
        bombObj.SetStat(stat,worldIndex);
    }
}
