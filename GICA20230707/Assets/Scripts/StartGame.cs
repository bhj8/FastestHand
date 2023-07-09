using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 需要这个命名空间来加载场景

public class StartGame : MonoBehaviour
{
    // 需要对这些场景名称进行修改，以符合你的实际场景名称
    private string practiceScene = "Practice";
    private string casualRankScene = "Relax";
    private string expertCompetitiveScene = "ExpertCompetitive";
    private string leaderboardScene = "Rank";

    public void LoadSPracticeGame()
    {
        SoundManager.instance.PlayMenuButton();
        // 加载练习场景
        SceneManager.LoadScene(practiceScene);
    }

    public void LoadSCasualRankGame()
    {
        SoundManager.instance.PlayMenuButton();
        // 加载休闲排位场景
        SceneManager.LoadScene(casualRankScene);
    }

    public void LoadSExpertCompetitiveGame()
    {
        SoundManager.instance.PlayMenuButton();
        // 加载专家竞技场景
        SceneManager.LoadScene(expertCompetitiveScene);
    }

    public void LoadSLeaderboard()
    {
        SoundManager.instance.PlayMenuButton();
        // 加载排行榜场景
        SceneManager.LoadScene(leaderboardScene);
    }

    public void QuitGame()
    {
        SoundManager.instance.PlayMenuButton();
        // 退出游戏
        Application.Quit();
    }




    public GameObject usernameInputField;

public void Start()
{
    // 从PlayerPrefs中获取username
    string username = PlayerPrefs.GetString("username", "");
    // 如果username不为空，则将其设置到输入框中
    if (username == ""){        
        // 如果username为空，则生成一个随机的username
        username = "单身狗" + UnityEngine.Random.Range(100, 999).ToString();
    }
    if (username != "")
    {   
        usernameInputField.GetComponent<UnityEngine.UI.InputField>().text = username;
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.Save();
    }
}


    public void OnUsernameInputEnd()
    {
        // 当输入框结束编辑时，将输入框中的内容保存到PlayerPrefs中
        string username = usernameInputField.GetComponent<UnityEngine.UI.InputField>().text;
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.Save();
    }
}
