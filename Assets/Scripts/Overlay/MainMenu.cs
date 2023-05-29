using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;

    public GameObject transitionObject;

    public float transitionTime = 1f;
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
        Debug.Log("Quitting");
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadLevel (int levelIndex)
    {
        transitionObject.SetActive(true);
        transitionObject.GetComponent<Animator>().SetTrigger("Start");
        yield  return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
