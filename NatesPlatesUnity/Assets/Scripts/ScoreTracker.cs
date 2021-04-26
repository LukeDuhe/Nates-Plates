using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    private int score = 0;
    public Text text;
    private GameMaster gm;

    private void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Score: {score:D8}";
    }

    public void AddPoints(int points)
    {
        if (gm.GetStage() == 0)
        {
            score += points;
        }
        else
        {
            score += points * 8 * gm.GetStage();
        }
    }
}
