using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseButton : MonoBehaviour
{
    // 定义一个静态的事件，所有的BaseButton都会使用这个事件
    public static UnityEvent onAnyButtonClick = new UnityEvent();

    public void ButtonClicked()
    {
        // 当按钮被点击时，触发静态事件
        onAnyButtonClick.Invoke();
        DeleteButton();
    }

    //删除按钮
    public void DeleteButton()
    {
        Destroy(gameObject);
    }
}