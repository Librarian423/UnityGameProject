using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public enum Scenes : int
    {
        None = -1,
        Title,
        GameScene,
        GameOver,
    }

    public void OnChangeScene(Scenes scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public void Change(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
