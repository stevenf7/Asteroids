using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreboardController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Scoreboard;

    public TextMesh text;
   
    void Start()
    {
        gameObject.SetActive(true);
        Scoreboard.SetActive(false);
    }

    public void SetObjectActive()
    {
        Scoreboard.SetActive(true);
   
        text.text = "Press the ball to continue";

   
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
