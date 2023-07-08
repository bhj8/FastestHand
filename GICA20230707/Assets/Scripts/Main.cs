using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    public GameObject timeText;
    private bool gameStarted = false;
    void start()
    {

    }

    void Update()
    {
        if (gameStarted==true) {
            timeText.GetComponent<Text>().text = Time.time.ToString();}
    }






    private void OnEnable()
    {
        BaseButton.onAnyButtonClick.AddListener(RespondToButtonClick);
    }

    private void OnDisable()
    {
        BaseButton.onAnyButtonClick.RemoveListener(RespondToButtonClick);
    }

    private void RespondToButtonClick()
    {
        gameStarted = true;
    }
}