using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectorUI : MonoBehaviour
{
    public GameObject optionPrefab;
    public Transform prevCharacter;
    public Transform selectedCharacter;
    private void Start()
    {
        Debug.Log(CharacterSelector.instance.characters.Count());
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
                //characterPrefab.transform.Find("RangeParent/TestSpell").gameObject.SetActive(true);
                break;
            case ("Shield"):
                //Debug.Log("RangeNow");
                characterPrefab.transform.Find("ShieldProtector").gameObject.SetActive(true);
                characterPrefab.GetComponentInChildren<ShieldProtector>().maxObjects = 5; 
                characterPrefab.GetComponentInChildren<ShieldProtector>().orbitRadius = 12;
                characterPrefab.GetComponentInChildren<ShieldProtector>().spawnDelay = 0.001f;
                StartCoroutine(TempFix(characterPrefab));
                //characterPrefab.transform.Find("RangeParent/TestSpell").gameObject.SetActive(true);
                break;
            default:
                //Debug.Log("4to-to ne tak");
                break;
        }
    }
    IEnumerator TempFix(GameObject pref)
    {
        yield return new WaitForSeconds(1f);
        foreach (Transform ttc in pref.GetComponentInChildren<ShieldProtector>().gameObject.transform)
        {
            GameObject circ = ttc.gameObject;
            circ.transform.localScale = new Vector3(0.025f, 0.025f, 1f);
            circ.transform.position = new Vector3(0f, 0f, -2f);
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
