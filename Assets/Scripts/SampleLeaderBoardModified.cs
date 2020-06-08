/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using Oculus.Platform;
using Oculus.Platform.Models;

// Coordinates updating leaderboard scores and polling for leaderboard updates.
public class SampleLeaderBoardModified
{
    // API NAME for the leaderboard where we store how many matches the user has won
    private const string MOST_MATCHES_WON = "MOST_MATCHES_WON";

    ///...

    // the top number of entries to query
    private const int TOP_N_COUNT = 5;

    // how often to poll the service for leaderboard updates
    private const float LEADERBOARD_POLL_FREQ = 30.0f;

    // the next time to check for leaderboard updates
    private float m_nextCheckTime;

    ///...

    // whether we've found the local user's entry yet
    private bool m_foundLocalUserMostWinsEntry;

    // number of times the local user has won
    private long m_numWins;

    // callback to deliver the most-wins leaderboard entries
    private OnMostWinsLeaderboardUpdated m_mostWinsCallback;

    ///...

    public void CheckForUpdates()
    {
        if (Time.time >= m_nextCheckTime &&
            PlatformManager.CurrentState == PlatformManager.State.WAITING_TO_PRACTICE_OR_MATCHMAKE)
        {
            m_nextCheckTime = Time.time + LEADERBOARD_POLL_FREQ;

            QueryMostWinsLeaderboard();
            QueryHighScoreLeaderboard();
        }
    }

    #region Most Wins Leaderboard

    public delegate void OnMostWinsLeaderboardUpdated(SortedDictionary<int, LeaderboardEntry> entries);

    public OnMostWinsLeaderboardUpdated MostWinsLeaderboardUpdatedCallback
    {
        set { m_mostWinsCallback = value; }
    }

    void QueryMostWinsLeaderboard()
    {
        // if a query is already in progress, don't start a new one.
        if (m_mostWins != null)
            return;

        m_mostWins = new SortedDictionary<int, LeaderboardEntry>();
        m_foundLocalUserMostWinsEntry = false;

        Leaderboards.GetEntries(MOST_MATCHES_WON, TOP_N_COUNT, LeaderboardFilterType.None,
            LeaderboardStartAt.Top).OnComplete(MostWinsGetEntriesCallback);
    }

    void MostWinsGetEntriesCallback(Message<int, LeaderboardEntry> msg)
    {
        if (!msg.IsError)
        {
            foreach (LeaderboardEntry entry in msg.Data)
            {
                m_mostWins[entry.Rank] = entry;

                if (entry.User.ID == PlatformManager.MyID)
                {
                    m_foundLocalUserMostWinsEntry = true;
                    m_numWins = entry.Score;
                }
            }

            // results might be paged for large requests
            if (msg.Data.HasNextPage)
            {
                Leaderboards.GetNextEntries(msg.Data).OnComplete(MostWinsGetEntriesCallback);
                return;
            }

            // if local user not in the top, get their position specifically
            if (!m_foundLocalUserMostWinsEntry)
            {
                Leaderboards.GetEntries(MOST_MATCHES_WON, 1, LeaderboardFilterType.None,
                    LeaderboardStartAt.CenteredOnViewer).OnComplete(MostWinsGetEntriesCallback);
                return;
            }
        }
        // else an error is returned if the local player isn't ranked - we can ignore that

        if (m_mostWinsCallback != null)
        {
            m_mostWinsCallback(m_mostWins);
        }
        m_mostWins = null;
    }

    #endregion

    // submit the local player's match score to the leaderboard service
    public void SubmitMatchScores(bool wonMatch, uint score)
    {
        if (wonMatch)
        {
            m_numWins += 1;
            Leaderboards.WriteEntry(MOST_MATCHES_WON, m_numWins);
        }

        if (score > 0)
        {
            Leaderboards.WriteEntry(HIGHEST_MATCH_SCORE, score);
        }
    }
}
*/