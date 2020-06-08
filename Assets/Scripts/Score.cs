using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int StartingScore = 0;
    // Start is called before the first frame update
    public Text scoreText;

    public int StartingLife = 5;
    private float transparency = 0f;
    public Text lifeText;

    private void Start()
    {
        FindObjectOfType<DamageOverlay>().AdjustColor(0,0);
    }
    private void Awake()
    {
        
        Interactable.m_incrementScore += increment;
    }

    private void OnDestroy()
    {
        Interactable.m_incrementScore -= increment;
    }
    private void Update()
    {
        //check for action

        //if there is an action, increment score


        scoreText.text = "Score: " + StartingScore.ToString();
        lifeText.text = "Life: " + StartingLife.ToString();
        //display updated score
    }
    // Update is called once per frame

    private void increment()
    {
        StartingScore += 10;
    }

    public void add_life()
    {
        StartingLife++;
        if (StartingLife > 3)
        {
            FindObjectOfType<DamageOverlay>().AdjustColor(0, 0);
            lifeText.color = Color.green;
        }
        lifeText.text = "Life: " + StartingLife.ToString();
    }

    public void subtract_life()
    {

            FindObjectOfType<AudioManager>().Play("Collision");

        StartingLife--;
        if (StartingLife > 3)
        {
            FindObjectOfType<DamageOverlay>().AdjustColor(0,0);
           
            return;
        }
        lifeText.color = Color.red;
        transparency = 1 - (2.5f* StartingLife / 10);
        FindObjectOfType<DamageOverlay>().AdjustColor(transparency, transparency - 0.3f);
        if (StartingLife == 1)
        {
            FindObjectOfType<AudioManager>().Play("heartbeat");
        }
        if (StartingLife <= 0)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    

}
