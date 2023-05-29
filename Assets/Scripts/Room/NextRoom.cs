using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NextRoom : PlayerExist
{
    public GameObject timerPanel;
    public Text timerNextText;

    [SerializeField]
    private float TeleportationDelay;

    private IEnumerator launchScriptCoroutine;

    public GameObject nextRoom;
    new void Start()
    {
        timerPanel = GameObject.Find("UI_Overlay").GetComponent<OverlayUI>().timerPanel;
        timerNextText = GameObject.Find("UI_Overlay").GetComponent<OverlayUI>().timerNextText;

        timerNextText.text = "";
        timerPanel.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            timerPanel.SetActive(true);
            if (launchScriptCoroutine != null)
            {
                StopCoroutine(launchScriptCoroutine);
            }
            launchScriptCoroutine = LaunchScriptAfterDelay();
            StartCoroutine(launchScriptCoroutine);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            timerPanel.SetActive(false);
            // If coroutine is still running, stop it
            if (launchScriptCoroutine != null)
            {
                StopCoroutine(launchScriptCoroutine);
                launchScriptCoroutine = null;
            }
        }
    }
    private void LoadNextRoom()
    {
        GameObject oldRG = GameObject.Find("RoomGenerator");
        GameObject oldSG = GameObject.Find("ShopGenerator(Clone)");
        GameObject DropList = GameObject.Find("DropList");
        foreach (Transform child in DropList.transform)
        {
            Destroy(child.gameObject);
        }
        if (oldRG == null)
        {
            oldRG = GameObject.Find("RoomGenerator(Clone)");
        }
        Transform playerTP = player.transform;
        playerTP.position = new Vector3 (0.175f, 0.25f, 0f);
        Destroy(oldRG);
        Destroy(oldSG);
        //Debug.Log("Adding " + PlayerPrefs.GetInt("RoomsCount") + " + 1 ");
        PlayerPrefs.SetInt("RoomsCount", PlayerPrefs.GetInt("RoomsCount") + 1);
        Instantiate(nextRoom, new Vector3(0.5f,0.5f,0), Quaternion.identity);
    }

    private IEnumerator LaunchScriptAfterDelay()
    {
        float timer = TeleportationDelay;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            timerNextText.text = "Time: " + timer.ToString("0.00");
            yield return null;
        }
        timerNextText.text = "Time: 0.00";
        
        //Debug.Log("TP");
        
        LoadNextRoom();
        launchScriptCoroutine = null;
    }
    void PrintChildObjects(GameObject parentObject)
    {
        foreach (Transform childTransform in parentObject.transform)
        {
            Debug.Log(childTransform.gameObject.name);
        }
    }
    void Update()
    {
    }
}
