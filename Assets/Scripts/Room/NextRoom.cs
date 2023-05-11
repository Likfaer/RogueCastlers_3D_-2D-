using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NextRoom : MonoBehaviour
{
    public float TeleportationDelay;

    private bool inTrigger;
    private IEnumerator launchScriptCoroutine;

    private GameObject UI_Overlay;
    private Transform UItoPanel;
    private GameObject timerPanel;
    private Transform PaneltoText;
    private Text timerNextText;

    public GameObject nextRoom;

    void Start()
    {
        UI_Overlay = GameObject.Find("UI_Overlay");
        UItoPanel = UI_Overlay.transform.Find("TimerPanel");
        timerPanel = UItoPanel.gameObject;
        PaneltoText = timerPanel.transform.Find("TimerText");
        timerNextText = PaneltoText.GetComponent<Text>();
        timerNextText.text = "";
        timerPanel.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
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
            inTrigger = false;
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
        GameObject player = GameObject.Find("Player");
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
