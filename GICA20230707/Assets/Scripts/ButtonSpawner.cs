using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    public GameObject[] buttonPrefabs;
    public RectTransform canvasTransform;

    private GameObject SpawnButton(Vector2 position, Vector2 size,GameObject buttonPrefab )
    {
        // 创建按钮
        GameObject button = Instantiate(buttonPrefab, canvasTransform);

        // 获取RectTransform组件以便设置大小和位置
        RectTransform rectTransform = button.GetComponent<RectTransform>();

        // 设置大小
        rectTransform.sizeDelta = size;

        // 设置锚点
        rectTransform.anchorMin = position;
        rectTransform.anchorMax = position;

        // 设置偏移量为0，使得锚点位于按钮的中心
        rectTransform.anchoredPosition = Vector2.zero;

        // 设置按钮的大小和位置
        rectTransform.offsetMin = new Vector2(-size.x / 2, -size.y / 2);
        rectTransform.offsetMax = new Vector2(size.x / 2, size.y / 2);

        // 返回创建的按钮
        return button;
    }

    private void CreateRandomButton(GameObject buttonPrefab)
{
    // 随机位置
    Vector2 position = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

    // 随机大小
    Vector2 size = new Vector2(Random.Range(50f, 200f), Random.Range(50f, 200f));

    // 创建按钮
    GameObject button = SpawnButton(position, size,buttonPrefab);

    // 在这里，你可以添加更多针对新创建的按钮的操作
}

    public void CreateNextButton( int index)
    {
        CreateRandomButton(buttonPrefabs[index]);
    }
}
