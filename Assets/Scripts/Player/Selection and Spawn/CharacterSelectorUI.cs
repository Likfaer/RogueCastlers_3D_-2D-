using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectorUI : MonoBehaviour
{
    public GameObject optionPrefab;
    public Transform prevCharacter;
    public Transform selectedCharacter;
    private void Start()
    {
        foreach (Character c in CharacterSelector.instance.characters)
        {
            GameObject option = Instantiate(optionPrefab, transform);

            Text text = option.GetComponentInChildren<Text>();
            text.text = c.name;

            ReplaceSquareToPlayer(option, c);

            Button button = option.GetComponentInChildren<Button>();

            button.onClick.AddListener(() =>
            {
                CharacterSelector.instance.SetSelectedObject(c.name);

                if (selectedCharacter != null)
                {
                    prevCharacter = selectedCharacter;
                }

                selectedCharacter = option.transform;
            });

        }
    }
    private void ReplaceSquareToPlayer(GameObject option, Character charact)
    {
        GameObject characterPrefab = Instantiate(charact.prefab, option.transform);
        characterPrefab.transform.localScale = new Vector3(1000f, 1000f, 1000f); // Adjust the scale as needed
        switch (charact.name)
        {
            case ("Melee"):
                //Debug.Log("MeleeNow");
                characterPrefab.transform.Find("WeaponParent").gameObject.SetActive(true);
                break;
            case ("Range"):
                //Debug.Log("RangeNow");
                characterPrefab.transform.Find("RangeParent").gameObject.SetActive(true);
                break;
            default:
                //Debug.Log("4to-to ne tak");
                break;
        }
    }

    private void Update()
    {
        if( selectedCharacter != null)
        {
            selectedCharacter.localScale = Vector3.Lerp(selectedCharacter.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 10);
        }
        if (prevCharacter != null)
        {
            prevCharacter.localScale = Vector3.Lerp(prevCharacter.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10);
        }
    }
}
