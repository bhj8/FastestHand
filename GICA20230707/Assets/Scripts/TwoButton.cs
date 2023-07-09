using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 导入 UnityEngine.UI 命名空间

public class TwoButton : BaseButton
{
    // 设置一个计数器来跟踪点击次数
    private int clickCount = 0;

    // 存储 Image 组件的引用
    private Image buttonImage;

    private void Awake()
    {
        // 获取 Image 组件的引用，如果 Image 是子对象，使用 GetComponentInChildren<Image>()
        buttonImage = GetComponent<Image>();
    }

    // 重写 ButtonClicked 方法，使其满足两次点击才删除的条件
    public override void ButtonClicked()
    {
        // 每点击一次，计数器增加1
        clickCount++;

        // 第一次点击后，改变 Image 的颜色
        if (clickCount == 1)
        
        {
            SoundManager.instance.PlayButtonDeleteSound();
            buttonImage.color = new Color32(0x00, 0xFF, 0x0B, 0xFF);
        }

        // 当按钮被点击两次时，触发静态事件并删除按钮
        if (clickCount == 2)
        {
            base.ButtonClicked();
        }
    }
}
