using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverlayUI : PlayerExist
{
    public GameObject timerPanel;
    public Text timerNextText;
    public GameObject UpgradePanel;
    public Text UpgradeNameText;
    public Text UpgradeValueText;
    public Text UpgradeCostText;
    
    //PauseMenu

    public static bool GameIsPaused = false;
    public bool PlayerDead = false;

    public GameObject pauseMenuUI;
    public GameObject statsPanelUI;
    public GameObject deadMenuUI;

    public float transitionTime = 1f;
    public Animator transition;

    public int roomsRecord = 0;

    new void Start()
    {
        timerPanel.SetActive(false);
        timerNextText.text = "";
        UpgradePanel.SetActive(false);
        UpgradeNameText.text = "";
        statsPanelUI.SetActive(false);
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
                    statsPanelUI.SetActive(false);
                }
                else
                {
                    Pause();
                    statsPanelUI.SetActive(true);
                }
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (player == null)
        {
            deadMenuUI.SetActive(true);
            PlayerPrefs.SetInt("totalRoomsCount", PlayerPrefs.GetInt("totalRoomsCount") + PlayerPrefs.GetInt("RoomsCount"));
            if (PlayerPrefs.GetInt("RoomsCount") > PlayerPrefs.GetInt("RoomsRecord"))
            {
                PlayerPrefs.SetInt("RoomsRecord", PlayerPrefs.GetInt("RoomsCount"));
            }
            PlayerPrefs.SetInt("RoomsCount", 0);
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
        PlayerDead = false;
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
