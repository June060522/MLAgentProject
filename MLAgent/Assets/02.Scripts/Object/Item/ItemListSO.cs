using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemList")]
public class ItemListSO : ScriptableObject
{
    public List<ItemObj> items;
}