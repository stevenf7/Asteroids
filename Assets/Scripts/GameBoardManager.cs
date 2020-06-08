using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;
using Oculus.Platform.Samples.EntitlementCheck;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
public class GameBoardManager : MonoBehaviour
{

    public string leaderboardname;
    public GameObject[] EntryObjects;
    List <LeaderboardEntry> lbe;
    public int amount;
    string sceneName;
    Scene m_Scene;
    private bool NotUpdated = true;
    private void Awake()
    {
        
        try
        {
            Core.AsyncInitialize();
            Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementCallback);
        } catch(UnityException e)
        {
            Debug.LogError("Platform failed to initialize due to exception");
            Debug.LogException(e);
            UnityEngine.Application.Quit();
        }
        
        
    }

    private void EntitlementCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("Not entitled to use this app");
            UnityEngine.Application.Quit();
        }
        else
        {
            Debug.LogError("Entitled to use");
        }
    }
    
    public void SubmitScore(string leaderboardname, int score)
    {
        if (score < 0)
        {
            return;
        }
        
        Leaderboards.WriteEntry(leaderboardname, score);
        Debug.Log("Data saved to leaderboards");
    }
    
    public void GetLeaderData (string leaderboardname)
    {
        lbe = new List<LeaderboardEntry>();
        Leaderboards.GetEntries(leaderboardname, amount, LeaderboardFilterType.None, LeaderboardStartAt.Top).OnComplete(LeaderboardGetCallback );
    }
    
    void LeaderboardGetCallback(Message<LeaderboardEntryList> msg)
    {
        if (!msg.IsError)
        {
            var entries = msg.Data;
            foreach (var entry in entries)
            {
                lbe.Add(entry);
            }
            Debug.Log("Leaderboards fetched successfully");
            UpdateUI();
        }
        else
        {
            Debug.LogError("Error getting leaderboards");
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i< EntryObjects.Length; i++)
        {
            if (i < lbe.Count)
            {
                EntryObjects[i].transform.GetChild(0).GetComponent<TextMesh>().text = "" + lbe[i].Rank;
                EntryObjects[i].transform.GetChild(2).GetComponent<TextMesh>().text = "" + lbe[i].User.OculusID;
                EntryObjects[i].transform.GetChild(1).GetComponent<TextMesh>().text = "" + lbe[i].Score;
            }
            else
            {
                EntryObjects[i].transform.GetChild(0).GetComponent<TextMesh>().text = "" + (i+1);
                EntryObjects[i].transform.GetChild(2).GetComponent<TextMesh>().text = "";
                EntryObjects[i].transform.GetChild(1).GetComponent<TextMesh>().text = "";
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetLeaderData(leaderboardname);
        
    }
    IEnumerator ExampleCoroutine(float time_)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 0.5 seconds.

        yield return new WaitForSeconds(time_);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    // Update is called once per frame
    void Update()
    {

        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        if (sceneName == "endGame" && NotUpdated)
        {
            StartCoroutine(ExampleCoroutine(0.5f));
            NotUpdated = false;

            GetLeaderData(leaderboardname);
            StartCoroutine(ExampleCoroutine(1f));
            GetLeaderData(leaderboardname);
        }
    }
}
