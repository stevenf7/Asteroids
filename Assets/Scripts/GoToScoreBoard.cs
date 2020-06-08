using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class GoToScoreBoard : MonoBehaviour
{
    float speed = 50f;


    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene("endGame");
    }

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
