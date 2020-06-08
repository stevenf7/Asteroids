using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{
    float speed = 50f;
    // Start is called before the first frame update
   /* private void Awake()
    {

        Interactable.m_incrementScore += GameScene;
    }
    private void OnDestroy()
    {
        Interactable.m_incrementScore -= GameScene;
    }


    private void GameScene()
    {
        SceneManager.LoadScene("Remastered");
    }

    void Start()
    {
        
    }
    */
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "endGame")
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
