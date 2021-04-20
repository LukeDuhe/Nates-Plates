using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AteFood : MonoBehaviour
{
    public ScoreTracker foodScore;
    public int pointValue;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Nate"))
        {
            foodScore.AddPoints(pointValue);
            Destroy(gameObject);
        }
    }
}
