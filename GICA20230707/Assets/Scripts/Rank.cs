using System;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.UI;


[Serializable]
public class ScoreData
{
    public string username;
    public float completion_time;
    public int id;
}

[Serializable]
public class ScoreDataWrapper
{
    public ScoreData[] scores;
}


public class Rank : MonoBehaviour
{
    public HttpHandler httpHandler;
    public GameObject scoreTextOBJ;
    private Text scoreText;

    void Awake()
    {
        scoreText = scoreTextOBJ.GetComponent<Text>();
    }

    void Start()
    {
        httpHandler.GetScore_Relax(OnGetScoreCompleted);
    }

    //回到主菜单
    public void LoadStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }

public void OnGetScoreCompleted(UnityWebRequest request)
{
    // 解析JSON
    string json = request.downloadHandler.text;
    ScoreDataWrapper data = JsonUtility.FromJson<ScoreDataWrapper>(json);

    // 转换为字符串并设置到scoreText中
    string scoreTextStr = data.scores
        .Select(score => $"{score.username}     {score.completion_time:F2}")  // F2 will format the time to keep 2 decimal places
        .Aggregate((a, b) => a + "\n" + b);
        
    scoreText.text = scoreTextStr;
}


}
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
