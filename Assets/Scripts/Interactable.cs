using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    public GameObject destroyedVersion;

   
    public static UnityAction m_incrementScore;
    public static UnityAction m_endGame;
    public static UnityAction m_incrementLife;
    private int life = 1;

    public float delay = 5f;
    private float countdown;
    public void Pressed()
    {
        Debug.Log("Interactable Pressed");
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        bool flip = !renderer.enabled;

        renderer.enabled = flip;
    }

    public void Destruction()
    {


        if (destroyedVersion != null)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
        }
        Destroy(gameObject);

        if (gameObject.GetComponent<MedKit>() != null)
        {

            //raise an action
            if (m_incrementLife != null)
            {
                m_incrementLife();
            }
        }

        //raise an action
        if (m_incrementScore != null)
        {
            m_incrementScore();
        }
        
        //if we are at main menu, or 
        if (SceneManager.GetActiveScene().name != "Remastered" && SceneManager.GetActiveScene().name != "Hammer")
        {
            SceneSwitcher();
        }

        


    }


    private void Start()
    {
        countdown = delay;
    }


    private void SceneSwitcher()
    {
        if (gameObject.name == "Start" || gameObject.name == "GameBox")
        {
           // SceneManager.LoadScene("Space");
           SceneManager.LoadScene("Remastered");
            return;
        }
        if (gameObject.name == "Gravity" || gameObject.name == "GravityBox")
        {
            SceneManager.LoadScene("Hammer");
            return;
        }
        if (gameObject.name == "Scoreboard")
        {
            SceneManager.LoadScene ("endGame");
            return;
        }

        if (gameObject.name == "MenuBox")
        {
            SceneManager.LoadScene("Menu");
            return;
        }
        if (gameObject.name == "Score")
        {
            SceneManager.LoadScene("endGame");
            return;
        }
       if (gameObject.name == "Help")
        {
            SceneManager.LoadScene("Help");
            return;
        }
       if (gameObject.name == "Credit")
        {
            SceneManager.LoadScene("Credit");
            return;
        }
    }

    public void Distance()
    {
        Vector3 position = gameObject.transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(position.x, 2) + Mathf.Pow(position.y, 2) + Mathf.Pow(position.y, 2));
        if (distance > 51)
        {
            Destroy(gameObject);
        }
        if (distance < 0.5)
        {
            Death();
            
        }
    }

    public void Death()
    {
        FindObjectOfType<Score>().subtract_life();
        life = FindObjectOfType<Score>().StartingLife;
        if (life > 0)
        {
            Destroy(gameObject);
            return;
        }
        Debug.LogWarning("Hit player");
        if (m_endGame != null)
        {
            m_endGame();
        }
    }
    public void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && SceneManager.GetActiveScene().name!= "Menu" && SceneManager.GetActiveScene().name != "endGame_Local" )
        {
            Destroy(gameObject);
        }
        Distance();
    }
}
