using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour {

    public void DoQuit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
