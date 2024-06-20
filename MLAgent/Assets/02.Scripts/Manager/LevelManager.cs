using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int level = 0;

    public int minGhost = 0;

    [SerializeField] TextMeshProUGUI leveltxt;
    [SerializeField] TextMeshProUGUI lefttxt;
    public void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        PlayerPrefs.SetInt("Stage", Mathf.Max(level, PlayerPrefs.GetInt("Stage",0)));
        if(minGhost <= 0)
        {
            StageClear();
        }
        leveltxt.text = $"Level : {level}";
        lefttxt.text = $"left : {minGhost}";
    }

    private void StageClear()
    {
        level++;
        minGhost = level + 2;
        FindObjectOfType<Player>().Reset();
    }
}
