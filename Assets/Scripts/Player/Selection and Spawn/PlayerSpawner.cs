using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerpref;
    private void Awake()
    {
        GameObject NakedPlayer = Instantiate(playerpref, transform.position, Quaternion.identity);
        NakedPlayer.transform.Find("PlayerStats").gameObject.SetActive(true);

        NakedPlayer.GetComponentInChildren<Animator>().enabled = true;
        NakedPlayer.GetComponentInChildren<PlayerInput>().enabled = true;

        if (CharacterSelector.instance == null)
        {
            //Debug.Log("unselected");
            NakedPlayer.transform.Find("WeaponParent").gameObject.SetActive(true);
            NakedPlayer.transform.Find("RangeParent").gameObject.SetActive(true);
        }
        else
        {
            //Debug.Log(CharacterSelector.instance.selectedStrObject);

            switch (CharacterSelector.instance.selectedStrObject)
            {
                case ("Melee"):
                    //Base Activations
                    NakedPlayer.transform.Find("WeaponParent").gameObject.SetActive(true);

                    //Movement Activations
                    NakedPlayer.GetComponent<AgentMover>().maxSpeed = 0.75f;

                    //Damage Stats
                    NakedPlayer.GetComponentInChildren<PlayerStats>().maxHealth = 200;
                    NakedPlayer.GetComponentInChildren<WeaponParent>().minDamage = 30;
                    NakedPlayer.GetComponentInChildren<WeaponParent>().maxDamage = 70;
                    break;



                case ("Range"):
                    //Base Activations
                    NakedPlayer.transform.Find("RangeParent").gameObject.SetActive(true);
                    NakedPlayer.transform.Find("TestSpell").gameObject.SetActive(true);

                    //Movement Activations
                    NakedPlayer.GetComponent<AgentMover>().maxSpeed = 0.55f;

                    //Damage Stats
                    NakedPlayer.GetComponentInChildren<PlayerStats>().maxHealth = 100;
                    NakedPlayer.GetComponentInChildren<TestSpell>().minDamage = 25;
                    NakedPlayer.GetComponentInChildren<TestSpell>().maxDamage = 35;
                    break;



                default:
                    //Debug.Log("4to-to ne tak");
                    //Base Activations
                    NakedPlayer.transform.Find("WeaponParent").gameObject.SetActive(true);
                    NakedPlayer.transform.Find("RangeParent").gameObject.SetActive(true);
                    NakedPlayer.transform.Find("TestSpell").gameObject.SetActive(true);

                    //Movement Activations


                    //Damage Stats

                    break;
            }

        }
        
    }
}
