using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool GameHasEnded = false;
    GameBoardManager lbm;
    //public float restartDelay = 4f;
    [SerializeField]
    public string leaderboardName = "Highest Score";
    [SerializeField]
    public Animator transition;
    public int GameScore = 0;
    private int highestScore = 0;
    private void Start()
    {
        lbm = GetComponent<GameBoardManager>();
        highestScore = PlayerPrefs.GetInt("HighScore", 0);

    }

    IEnumerator LoadNextLevel()
    {
        transition.SetTrigger("Start");
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 0.5 seconds.

        yield return new WaitForSeconds(1f);


        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    private void Awake()
    {

        Interactable.m_endGame += EndGame;
    }
    private void OnDestroy()
    {
        Interactable.m_endGame -= EndGame;
    }

    public void EndGame()
    {
        if (GameHasEnded == false)
        {

            GameScore = FindObjectOfType<Score>().StartingScore;
            lbm.SubmitScore(leaderboardName, GameScore);
            GameHasEnded = true;
            Debug.LogWarning("END GAME");
            PlayerPrefs.SetInt("Score", GameScore);
            
            if (GameScore > highestScore)
            {
                PlayerPrefs.SetInt("HighScore", GameScore);
            }

            FindObjectOfType<Spawn>().PauseSpawning();
            FindObjectOfType<DamageOverlay>().setObjectInactive();
            FindObjectOfType<CanvasboardController>().SetObjectInactive();
            FindObjectOfType<ScoreboardController>().SetObjectActive();
            
        }

    }


}
