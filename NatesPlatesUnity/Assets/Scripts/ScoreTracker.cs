using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    private int score = 0;
    public Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = $"Score: {score:D8}";
    }

    public void AddPoints(int points)
    {
        score += points;
    }
}
