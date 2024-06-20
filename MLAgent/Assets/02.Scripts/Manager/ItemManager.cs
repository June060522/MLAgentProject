using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemListSO itemList;
    Player player;

    private void Start()
    {
        player = transform.parent.parent.GetComponentInChildren<Player>();
    }

    public void CreateItem(Vector3 position)
    {
        int index = Random.Range(0,itemList.items.Count);
        Vector2Int positionIndex = player.positionManager.GetPositionIndex(position);

        Vector3 worldPosition = player.positionManager.GetWorldPosition(positionIndex);
        worldPosition.y += 1.0f;
        Instantiate(itemList.items[index],worldPosition,Quaternion.identity, player.game.parent);
    }
}
