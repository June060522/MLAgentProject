using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [SerializeField]Transform parent;
    [SerializeField]GameObject environment;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError($"{transform} : StageManager Is Multiply Running!");
        }
    }

    private void Init()
    {
        Instantiate(environment,parent);
    }
}
