using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class SuperWeapon : MonoBehaviour
{
    

    int scoreneeded;

    public int nukes_available;

    public Text nukeNumberText;
    // Start is called before the first frame update
    void Start()
    {
        scoreneeded = 0;
    }

    // Update is called once per frame
    void Update()
    {
        check_nuke_available();
    }

    private void Awake()
    {

        Interactable.m_incrementScore += increment;
    }

    private void OnDestroy()
    {
        Interactable.m_incrementScore -= increment;
    }
    void check_nuke_available()
    {
        if (scoreneeded >= 1000)
        {
            scoreneeded -= 1000;
            nukes_available++;
        }
        nukeNumberText.text ="Super Weapons: " + nukes_available.ToString();
    }

    private void increment()
    {
        scoreneeded += 10;
    }
}
