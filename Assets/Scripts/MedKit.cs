using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    // Start is called before the first frame update
    
    void addlife()
    {
        FindObjectOfType<Score>().add_life();
        OnDestroy();
        Destroy(gameObject);
    }

    private void Awake()
    {

        Interactable.m_incrementLife+= addlife;
    }

    private void OnDestroy()
    {
        Interactable.m_incrementLife -= addlife;
    }

}
