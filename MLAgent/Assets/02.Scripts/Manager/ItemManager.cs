using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [SerializeField] private ItemListSO itemList;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Debug.LogError($"{transform} : ItemManager is multiply running!");
            Destroy(gameObject);
        }
    }

    public void CreateItem(Vector3 position)
    {
        int index = Random.Range(0,itemList.items.Count);
        Vector2Int positionIndex = PositionManager.Instance.GetPositionIndex(position);

        Vector3 worldPosition = PositionManager.Instance.GetWorldPosition(positionIndex);

        Instantiate(itemList.items[index],worldPosition,Quaternion.identity);
    }
}
