using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endGame_Local : MonoBehaviour
{
    public TextMesh HighScore;
    public TextMesh score;
    
    // Start is called before the first frame update
    void Start()
    {
        HighScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        score.text = PlayerPrefs.GetInt("Score", 0).ToString();
    }


}
