using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    HashSet<Player> players = new HashSet<Player>();

    [SerializeField] Vector2Int[] playerSpot;
    [SerializeField] GameObject[] playerList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError($"{transform} : Game is multiply running!");
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (playerList.Length != playerSpot.Length)
            Debug.LogError("The lengths of the two arrays are different!");

        for(int i = 0 ; i < playerList.Length; i++)
        {
            GameObject player = Instantiate(playerList[i], PositionManager.Instance.GetWorldPosition(playerSpot[i]),Quaternion.identity);
        }
    }
}