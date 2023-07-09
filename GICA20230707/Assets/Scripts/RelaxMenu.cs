using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelaxMenu : MonoBehaviour
{
    //重新载入场景
    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }


    //载入到初始场景
    public void LoadStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }

    //载入排行榜场景
    public void LoadRankScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Rank");
    }

}
