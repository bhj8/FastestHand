using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedButton : BaseButton
{
    private Image buttonImage;
    private Text countDownText;
    private float countdownTimer = 1f; // 初始倒计时时间，可以根据需要调整
    private float elapsedTimer = 0f; // 已过去的时间
    private bool isCountdownEnded = false;

    private void Awake()
    {
        // 获取 Text 组件的引用
        countDownText = GetComponentInChildren<Text>();
        
        // 获取 Image 组件的引用
        buttonImage = GetComponent<Image>();

        // 设置初始颜色为红色
        buttonImage.color = Color.red;
    }

    private void Update()
    {
        // 如果倒计时还没有结束，就更新倒计时
        if (!isCountdownEnded)
        {
            // 更新已过去的时间
            elapsedTimer += Time.deltaTime;
            
            if (elapsedTimer >= countdownTimer)
            {
                // 倒计时结束后，改变颜色为绿色，并设置标志位
                buttonImage.color = Color.green;
                isCountdownEnded = true;
            }

            // 更新倒计时文本
            countDownText.text = (countdownTimer - elapsedTimer).ToString("0.00");
        }
        else
        {
            // 倒计时结束后，隐藏这个文本
countDownText.gameObject.SetActive(false);

        }
    }

    public override void ButtonClicked()
    {
        // 如果倒计时还没结束，点击按钮时减少倒计时时间
        if (!isCountdownEnded)
        {
            countdownTimer += 0.5f; // 增加0.5秒，可以根据需要调整
        }
        else
        {
            // 倒计时结束后，触发事件并删除按钮
            base.ButtonClicked();
        }
    }
}
