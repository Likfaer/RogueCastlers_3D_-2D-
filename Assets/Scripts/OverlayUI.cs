using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayUI : MonoBehaviour
{
    private GameObject timerPanel;
    private Text timerNextText;
    void Start()
    {
        timerPanel = GameObject.Find("TimerPanel");
        timerNextText = GameObject.Find("TimerText").GetComponent<Text>();
        timerNextText.text = "";
        timerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
