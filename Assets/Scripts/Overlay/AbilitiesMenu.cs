using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class Ability
{
    public Sprite sprite;
    public string description;
}
public class AbilitiesMenu : MonoBehaviour
{
    public List<Ability> abilities;
    public static AbilitiesMenu instance;
    public GameObject AbiOptionPrefab;
    public GameObject descriptionText;

    public Transform prevUnit;
    public Transform selectedUnit;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (Ability c in abilities)
        {
            GameObject option = Instantiate(AbiOptionPrefab, transform);

            Image sprite = option.GetComponentInChildren<Image>();
            sprite.sprite = c.sprite;

            Text text = option.GetComponentInChildren<Text>();
            text.text = c.sprite.name;


            //ReplaceToAbility(option, c);

            Button button = option.GetComponentInChildren<Button>();

            button.onClick.AddListener(() =>
            {
                ShowAbilityInformation(c);
                //DisplayOnCharacter(c);

                if (selectedUnit != null)
                {
                    prevUnit = selectedUnit;
                }

                selectedUnit = option.transform;
            });

        }
    }
    private void ShowAbilityInformation(Ability ability)
    {
        descriptionText.GetComponent<Text>().text = ability.description;
    }

    private void DisplayOnCharacter(GameObject ability)
    {
        // Implement code to visually display the ability on the character
    }
    private void Update()
    {
        if (selectedUnit != null)
        {
            selectedUnit.localScale = Vector3.Lerp(selectedUnit.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 10);
        }
        if (prevUnit != null)
        {
            prevUnit.localScale = Vector3.Lerp(prevUnit.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10);
        }
    }
}
