using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class OverlayUI : PlayerExist
{
    public GameObject timerPanel;
    public Text timerNextText;

    //PauseMenu

    public static bool GameIsPaused = false;
    public bool PlayerDead = false;

    public GameObject pauseMenuUI;
    public GameObject deadMenuUI;

    public float transitionTime = 1f;
    public Animator transition;

    new void Start()
    {
        timerPanel = GameObject.Find("UI_Overlay/TimerPanel");
        timerNextText = GameObject.Find("UI_Overlay/TimerPanel/TimerText").GetComponent<Text>();    
        timerNextText.text = "";
        timerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (player == null)
        {
            deadMenuUI.SetActive(true);
            PlayerDead = true;
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadRestart()
    {
        Time.timeScale = 1f;
        //SetDefaults();
        StartCoroutine(LoadLevel(1));
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        //SetDefaults();
        StartCoroutine(LoadLevel(0));
    }
    public void QuitGame()
    {

        Application.Quit();
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
