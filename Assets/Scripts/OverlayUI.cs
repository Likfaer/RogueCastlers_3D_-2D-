using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class OverlayUI : MonoBehaviour
{
    private GameObject timerPanel;
    private Text timerNextText;

    //PauseMenu

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public float transitionTime = 1f;
    public Animator transition;

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
        if(Input.GetKeyDown(KeyCode.Escape))
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

    public void LoadMenu()
    {
        Time.timeScale = 1f;
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
