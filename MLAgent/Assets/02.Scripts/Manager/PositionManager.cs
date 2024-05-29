using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public static PositionManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"{transform} : PositionManager is multiply running!");
            Destroy(gameObject);
        }
    }

    public Vector3 GetWorldPosition(Vector2Int index, float y = -8.5f)
    {
        return new Vector3(index.x * 2, y, index.y * 2);
    }

    public Vector2Int GetPositionIndex(Vector3 position)
    {
        return new Vector2Int(Mathf.RoundToInt(position.x / 2.0f), Mathf.RoundToInt(position.z / 2.0f));
    }
}