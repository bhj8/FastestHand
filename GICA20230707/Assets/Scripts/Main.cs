using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject timeText;
    private bool gameStarted = false;
    private float gameStartTime;
    private float gameEndTime;
public int level;

    public ButtonSpawner buttonSpawner;
    private Queue<int> levelDataQueue;

    void Awake()
    {
        buttonSpawner = GetComponent<ButtonSpawner>();
        uploadScoreText = uploadScore.GetComponent<Text>();
    }
    void Start()
    {
        levelDataQueue = new Queue<int>(Levels.GetLevelData(level));
    }

    private string[] zhanji = new string[5] { "战绩上传中.", "战绩上传中..", "战绩上传中...", "战绩上传中....", "战绩上传中....." };
    private bool isUpload = false;
    public Text uploadScoreText;
    private float timer = 0;
    private int index = 0;

    void Update()
    {
        if (!isUpload)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                timer = 0;
                uploadScoreText.text = zhanji[index];
                index = (index + 1) % zhanji.Length; // 循环使用zhanji中的字符串
            }
        }
        // 这里是你的其他Update逻辑
        UpdateGameTime();
    }

    private void UpdateGameTime()
    {
        if (gameStarted)
        {
            // 计算游戏开始后经过的时间
            gameEndTime = Time.time - gameStartTime;
            // 显示时间
            timeText.GetComponent<Text>().text = gameEndTime.ToString("0.00");
        }
    }



    private void OnEnable()
    {
        BaseButton.onAnyButtonClick.AddListener(RespondToButtonClick);
    }

    private void OnDisable()
    {
        BaseButton.onAnyButtonClick.RemoveListener(RespondToButtonClick);
    }

    private bool isFirstTime = true;
    private int buttonCount = 1;

private void RespondToButtonClick()
{
    gameStarted = true;
    buttonCount--;
    // 如果是第一次调用这个方法
    if (isFirstTime)
    {
        Invoke("NextButton", UnityEngine.Random.Range(0.5f, 1f));
        Invoke("NextButton", UnityEngine.Random.Range(0.5f, 1f));

        timeText.SetActive(true);
        // 记录游戏开始的时间
        gameStartTime = Time.time;

        // 不再是第一次调用
        isFirstTime = false;
    }
    NextButton();
}




    private void NextButton()
    {
        if (levelDataQueue.Count > 0)
        {
            buttonCount++;
            // 提取一个元素并删除
            int nextData = levelDataQueue.Dequeue();

            // 处理 nextData
            buttonSpawner.CreateNextButton(nextData);
        }
        else
        {
            if (buttonCount <= 0)
            {
                GameEnd();
            }
        }
        // // 从ButtonSpawner组件中获取CreateNextButton方法
        // System.Reflection.MethodInfo createNextButton = ButtonSpawner.GetType().GetMethod("CreateNextButton");
        // // 调用CreateNextButton方法
        // createNextButton.Invoke(ButtonSpawner, null);


    }



    public GameObject GameEndTips;
    public GameObject GameEndTime;

    private void GameEnd()
    {
        // 停止计时
        gameStarted = false;

        // 显示游戏结束的提示
        GameEndTips.SetActive(true);
        GameEndTime.GetComponent<Text>().text = gameEndTime.ToString("0.00");
        if (level != 0)
        {
            SendScore();
        }




    }


    public HttpHandler httpHandler;
    public string userName = "doge101";
    
    public GameObject uploadScore;


    [System.Serializable]
    public class ServerResponse
    {
        public int status;
        public string message;
    }

    public void SendScore()
    {
        //这里想加个回调，处理HTTP的状态码
        userName = PlayerPrefs.GetString("username", "");

        // 调用HttpHandler组件中的SendScore方法
        httpHandler.SendScore_Relax(gameEndTime, userName, (request) =>
        {

            isUpload = true;
            if (request.responseCode == 200)  // 200表示成功
            {
                ServerResponse serverResponse = JsonUtility.FromJson<ServerResponse>(request.downloadHandler.text);
                if (serverResponse.status == 1002)
                {
                    uploadScoreText.text = "用户名违规，上传失败";
                }
                else
                {
                    uploadScoreText.text = "战绩上传成功";
                }
            }
            else if (request.responseCode == 400) // 400表示客户端错误
            {
                uploadScoreText.text = "战绩上传失败";
            }
            else if (request.responseCode == 500) // 500表示服务器错误
            {
                uploadScoreText.text = "战绩上传失败";
            }
        });

    }

}
