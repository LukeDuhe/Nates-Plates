using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTester : MonoBehaviour
{
    public ScoreTracker score_tracker;

    public int points_to_add = 0;

    public bool add_points = false;

    // Update is called once per frame
    void Update()
    {
        if (add_points)
        {
            add_points = false;

            score_tracker.AddPoints(points_to_add);
        }
    }
}
