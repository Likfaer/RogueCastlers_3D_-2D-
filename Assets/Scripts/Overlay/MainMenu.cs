using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    public GameObject transitionObject;

    public float transitionTime = 1f;
    private void Start()
    {
        LoadSettings();
    }
    private void LoadSettings()
    {
        if (volumeSlider != null)
        {
            if (PlayerPrefs.HasKey("SettingsVolume"))
            {
                //Debug.Log("volume+");
                float volume = PlayerPrefs.GetFloat("SettingsVolume");
                SetVolume(volume);
                volumeSlider.value = volume;
            }

            if (PlayerPrefs.HasKey("SettingsFullscreen"))
            {
                //Debug.Log("fs+");
                bool isFullscreen = PlayerPrefs.GetInt("SettingsFullscreen") == 1;
                SetFullscreen(isFullscreen);
                fullscreenToggle.isOn = isFullscreen;
            }
        }
    }
    public void StartMenu()
    {
        StartCoroutine(LoadLevel(0));
    }
    public void StartGameSelection()
    {
        StartCoroutine(LoadLevel(1));
    }
    public void StartGame()
    {
        StartCoroutine(LoadLevel(2));
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void ExitGame()
    {
        //Debug.Log("Quitting");
        Application.Quit();
    }
    public void SetVolume(float volume)
    {
        GameObject.Find("ServerGameManager").GetComponent<MusicManager>().audioSource.volume = volume;
        //Debug.Log("try to save volume with :" + volume);
        PlayerPrefs.SetFloat("SettingsVolume", volume);
        //Debug.Log("volume saved with :" + PlayerPrefs.GetFloat("SettingsVolume"));
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        //Debug.Log("try to save isFS with :" + (isFullscreen ? 1 : 0));
        PlayerPrefs.SetInt("SettingsFullscreen", (isFullscreen ? 1 : 0));
        //Debug.Log("fs saved with :" + PlayerPrefs.GetInt("SettingsFullscreen"));
    }
    public void ApplySettings()
    {
        if (volumeSlider != null)
        {
            SetVolume(volumeSlider.value);
            SetFullscreen(fullscreenToggle.isOn);
        }
    }
    void Update()
    {
        
    }
    void OnApplicationQuit()
    {
        Debug.Log("saving settings");
        ApplySettings();
    }
    IEnumerator LoadLevel (int levelIndex)
    {
        transitionObject.SetActive(true);
        transitionObject.GetComponent<Animator>().SetTrigger("Start");
        yield  return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
