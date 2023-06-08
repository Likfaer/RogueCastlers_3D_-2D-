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
                    NakedPlayer.GetComponent<AgentMover>().maxSpeed = 0.85f;

                    //Damage Stats
                    NakedPlayer.GetComponentInChildren<PlayerStats>().maxHealth = 300;
                    NakedPlayer.GetComponentInChildren<WeaponParent>().minDamage = 50;
                    NakedPlayer.GetComponentInChildren<WeaponParent>().maxDamage = 90;
                    NakedPlayer.GetComponentInChildren<WeaponParent>().attackCooldown = 0.75f;
                    break;



                case ("Range"):
                    //Base Activations
                    NakedPlayer.transform.Find("RangeParent").gameObject.SetActive(true);
                    NakedPlayer.transform.Find("RangeParent/TestSpell").gameObject.SetActive(true);

                    //Movement Activations
                    NakedPlayer.GetComponent<AgentMover>().maxSpeed = 0.65f;

                    //Damage Stats
                    NakedPlayer.GetComponentInChildren<PlayerStats>().maxHealth = 175;
                    NakedPlayer.GetComponentInChildren<TestSpell>().minDamage = 55;
                    NakedPlayer.GetComponentInChildren<TestSpell>().maxDamage = 85;
                    NakedPlayer.GetComponentInChildren<TestSpell>().projectileForce = 0.9f;
                    NakedPlayer.GetComponentInChildren<TestSpell>().attackCooldown = 0.35f;
                    break;

                case ("Shield"):
                    //Base Activations
                    NakedPlayer.transform.Find("ShieldProtector").gameObject.SetActive(true);

                    //Movement Activations
                    NakedPlayer.GetComponent<AgentMover>().maxSpeed = 0.75f;

                    //Damage Stats
                    NakedPlayer.GetComponentInChildren<PlayerStats>().maxHealth = 250;
                    NakedPlayer.GetComponentInChildren<ShieldProtector>().maxObjects = 5;
                    NakedPlayer.GetComponentInChildren<ShieldProtector>().orbitRadius = 0.3f;
                    NakedPlayer.GetComponentInChildren<ShieldProtector>().orbitSpeed = 70f;
                    NakedPlayer.GetComponentInChildren<ShieldProtector>().damage = 15;
                    break;



                default:
                    //Debug.Log("4to-to ne tak");
                    //Base Activations
                    NakedPlayer.transform.Find("WeaponParent").gameObject.SetActive(true);
                    NakedPlayer.transform.Find("RangeParent").gameObject.SetActive(true);
                    NakedPlayer.transform.Find("RangeParent/TestSpell").gameObject.SetActive(true);

                    //Movement Activations


                    //Damage Stats

                    break;
            }

        }
        
    }
}
