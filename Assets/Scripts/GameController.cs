using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void moveToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
