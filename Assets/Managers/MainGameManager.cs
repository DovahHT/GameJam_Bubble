using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    private static MainGameManager instance;
    public static MainGameManager Instance => instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        Initiate();
    }


    private void Initiate()
    {
        MainMenuScreen.Instance.Show();
    }

    public void OpenGameScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
