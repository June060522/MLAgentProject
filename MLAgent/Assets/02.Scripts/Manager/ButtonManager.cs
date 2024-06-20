using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        text.text = "MaxStage : " + PlayerPrefs.GetInt("Stage", 0);
    }

    public void StartBtn()
    {
        SceneManager.LoadScene("Play");
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
