using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class GotoMainMenu : MonoBehaviour
{

    float speed = 50f;
    /*
    private void Awake()
    {

        Interactable.m_incrementScore += GameScene;
    }
    private void OnDestroy()
    {
        Interactable.m_incrementScore -= GameScene;
    }


    private void GameScene()
    {
        SceneManager.LoadScene("Menu");
    }
    */
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "endGame")
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
