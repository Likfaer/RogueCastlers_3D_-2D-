using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamManager : MonoBehaviour
{
    private static SteamManager instance;

    public static string steamID;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            SteamClient.Init(480);
            steamID = SteamClient.SteamId.ToString();
            gameObject.GetComponent<WebManager>().Register(steamID);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnApplicationQuit()
    {
        try
        {
            SteamClient.Shutdown();
        }
        catch
        {

        }
    }
}
