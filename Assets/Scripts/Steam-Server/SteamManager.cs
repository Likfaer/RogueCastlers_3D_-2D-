using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamManager : MonoBehaviour
{
    private static SteamManager instance;

    public static string steamID;
    private void Awake()
    {
        //pust budet na vsyakii
    }
    private void Start()
    {
        if (instance == null)
        {
            SteamClient.Init(480);
            steamID = SteamClient.SteamId.ToString();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        gameObject.GetComponent<WebManager>().Register(steamID);
    }
    private void OnApplicationQuit()
    {
        SteamClient.Shutdown();
       
    }
}
