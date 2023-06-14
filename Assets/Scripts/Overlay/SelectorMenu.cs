using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectorMenu : MonoBehaviour
{
    [SerializeField] private GameObject transitionObject;
    [SerializeField] private GameObject selectorMenu;
    [SerializeField] private GameObject abilitiesMenu;

    public float transitionTime = 1f;

    private void Start()
    {
        abilitiesMenu.SetActive(false);
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
    public void SwitchOnSelectorMenu()
    {
        if (selectorMenu.activeSelf == true)
        {
            selectorMenu.SetActive(false);
            abilitiesMenu.SetActive(true);
        }
        else
        {
            selectorMenu.SetActive(true);
            abilitiesMenu.SetActive(false);
        }
    }
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
