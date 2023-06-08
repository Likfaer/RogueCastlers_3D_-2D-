using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("RoomsCount", 0);
        PlayerPrefs.SetInt("totalRoomsCount", 0);
        PlayerPrefs.SetInt("RoomsRecord", 0);
    }
    void Awake()
    {

    }
    void Prints()
    {
        Debug.Log("RoomsCount:" + PlayerPrefs.GetInt("RoomsCount"));
        Debug.Log("RoomsRecord:" + PlayerPrefs.GetInt("RoomsRecord"));
        Debug.Log("totalRoomsCount:" + PlayerPrefs.GetInt("totalRoomsCount"));
    }
    public void AddNextRoom()
    {
        PlayerPrefs.SetInt("RoomsCount", PlayerPrefs.GetInt("RoomsCount") + 1);
        if (PlayerPrefs.GetInt("RoomsCount") > PlayerPrefs.GetInt("RoomsRecord"))
        {
            PlayerPrefs.SetInt("RoomsRecord", PlayerPrefs.GetInt("RoomsCount"));
        }
        //Prints();
    }
    public void SetOnReloadorQuit()
    {
        //Debug.Log("trying: " + PlayerPrefs.GetInt("totalRoomsCount") + " + " + PlayerPrefs.GetInt("RoomsCount"));
        PlayerPrefs.SetInt("totalRoomsCount", PlayerPrefs.GetInt("totalRoomsCount") + PlayerPrefs.GetInt("RoomsCount"));
        if (PlayerPrefs.GetInt("RoomsCount") > PlayerPrefs.GetInt("RoomsRecord"))
        {
            PlayerPrefs.SetInt("RoomsRecord", PlayerPrefs.GetInt("RoomsCount"));
        }
        PlayerPrefs.SetInt("RoomsCount", 0);
        //Prints();
    }
    void Update()
    {
        
    }
}
