using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class UserData
{
    public PlayerStatistics playerStats;
    public Error error;
}
[System.Serializable]
public class Error
{
    public string message; 
    public bool isError;
}
[System.Serializable]
public class PlayerStatistics
{
    public string steamID;
    public string timeplayed;
    public string roomsplayed;
    public string roomRecord;

    public PlayerStatistics(string steamid, string timepl, string roomrec)
    {
        steamID = steamid;
        timeplayed = timepl;
        roomRecord = roomrec;
    }
    public void SetsteamID(string value) => steamID = value;
    public void Settimeplayed(string value) => timeplayed = value;
    public void SetroomRecord(string value) => roomRecord = value;
}
public class WebManager : SteamManager
{
    public static UserData userData = new UserData();
    [SerializeField] string targetURL;
    [SerializeField] public UserData visData = userData;
    public string GetUserData(UserData data)
    {
        return JsonUtility.ToJson(data);
    }
    public UserData SetUserData(string data)
    {
        return JsonUtility.FromJson<UserData>(data);
    }
    private void Start()
    {
        userData.error = new Error() { message = "Text", isError = true };
        userData.playerStats = new PlayerStatistics("tempid", "-1", "-1");
    }
    public void Register(string steamID)
    {
        StopAllCoroutines();
        StartCoroutine(Registernew(steamID));
    }
    IEnumerator Registernew(string steamID)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "register");
        form.AddField("steam_id", steamID);
        using (UnityWebRequest www = UnityWebRequest.Post(targetURL, form))
        { 
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                userData = SetUserData(www.downloadHandler.text);
            }
        }

    }
    private void OnApplicationQuit()
    {
        StartCoroutine(Quit(SteamManager.steamID.ToString()));
    }
    public void testSend()
    {
        Debug.Log("send! " + SteamManager.steamID.ToString());
        StartCoroutine(Quit(SteamManager.steamID.ToString()));
    }
    IEnumerator Quit(string steamID)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "update");
        form.AddField("steam_id", steamID);

        float elapsedTime = Time.time;
        form.AddField("timeplayed", elapsedTime.ToString());
        Debug.Log("time" + elapsedTime.ToString());

        string roomsplayed = PlayerPrefs.GetInt("totalRoomsCount").ToString();
        form.AddField("roomsplayed", roomsplayed);
        Debug.Log("roomsplayed" + roomsplayed);
        PlayerPrefs.SetInt("totalRoomsCount", 0);

        string roomsrecord = PlayerPrefs.GetInt("roomsrecord").ToString();
        form.AddField("roomsrecord", roomsrecord);
        Debug.Log("roomsrecord " + roomsrecord);
        PlayerPrefs.SetInt("roomsrecord", 0);

        using (UnityWebRequest www = UnityWebRequest.Post(targetURL, form))
        {
            yield return www.SendWebRequest();  

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("1 : " + GetUserData(userData));
                Debug.Log("2 : " + www.downloadHandler.text);
                userData = SetUserData(www.downloadHandler.text);
                Debug.Log("3 : " + GetUserData(userData));
            }
        }

    }
}
