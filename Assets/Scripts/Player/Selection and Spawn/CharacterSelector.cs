using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public Character[] characters;

    public static CharacterSelector instance;
    public string selectedStrObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        } else{
            
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (characters.Length > 0)
        {
            selectedStrObject = "0";
        }
    }
    public void SetSelectedObject(string str)
    {
        instance.selectedStrObject = str;
    }
}
