using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YamlDotNet.Core.Tokens;

public class CurrencyChange : PlayerExist
{
    public enum pickupObject {COIN, GEM};
    public pickupObject currentObject;
    public int pickupQuantity;


    private bool isButtonPressed = false; // Track if the button is currently pressed
    private bool isPlayerOnObject = false; // Track if the player is on the object

    private GameObject UpgradePanel;
    private Text UpgradeNameText;
    //private Text UpgradeValueText;
    private Text UpgradeCostText;

    new private void Start()
    {
        UpgradePanel = GameObject.Find("UI_Overlay").GetComponent<OverlayUI>().UpgradePanel;
        UpgradeNameText = GameObject.Find("UI_Overlay").GetComponent<OverlayUI>().UpgradeNameText;
        //UpgradeValueText = GameObject.Find("UI_Overlay").GetComponent<OverlayUI>().UpgradeValueText;
        UpgradeCostText = GameObject.Find("UI_Overlay").GetComponent<OverlayUI>().UpgradeCostText;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerOnObject = true;

            if (gameObject.tag == "Autopickable")
            {
                PlayerStats.playerStats.AddCurrency(this);
                Destroy(gameObject);
            }
            else
            {
                float EventCounts = gameObject.GetComponent<PlayerUpgrades>().onPickUp.GetPersistentEventCount();

                UpgradeNameText.text = gameObject.name;

                gameObject.GetComponent<PlayerUpgrades>().onEnterTrigger?.Invoke();


                UpgradeCostText.text = pickupQuantity.ToString();

                UpgradePanel.SetActive(true);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UpgradePanel.SetActive(false);
            isPlayerOnObject = false;
        }
    }
    public void Update()
    {
        if (isPlayerOnObject)
        {
            if (gameObject.tag == "Autopickable")
            {
                PlayerStats.playerStats.AddCurrency(this);
                Destroy(gameObject);
            }
            else
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (this.currentObject == pickupObject.COIN)
                    {
                        //Debug.Log(PlayerStats.playerStats.coins + " + " + pickupQuantity + " > 0 ?");
                        if (PlayerStats.playerStats.coins + pickupQuantity >= 0)
                        {
                            if (gameObject.GetComponent<PlayerUpgrades>() != null)
                            {
                                gameObject.GetComponent<PlayerUpgrades>().onPickUp?.Invoke();
                            }
                            PlayerStats.playerStats.AddCurrency(this);
                            Destroy(gameObject);
                        }
                    }
                    if (this.currentObject == pickupObject.GEM)
                    {
                        if (PlayerStats.playerStats.gems + pickupQuantity >= 0)
                        {

                            if (gameObject.GetComponent<PlayerUpgrades>() != null)
                            {
                                gameObject.GetComponent<PlayerUpgrades>().onPickUp?.Invoke();
                            }
                            PlayerStats.playerStats.AddCurrency(this);
                            Destroy(gameObject);
                        }
                    }
                }
            }

        }
    }
}
