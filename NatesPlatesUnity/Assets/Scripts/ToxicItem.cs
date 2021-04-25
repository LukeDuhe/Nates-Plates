using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicItem : MonoBehaviour
{
    public int points = 0;
    private ScoreTracker scoreTracker;
    private bool isTouchingToxicWaste = false;


    private void Awake()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingToxicWaste)
        {
            scoreTracker.AddPoints(points);
            GetComponentInParent<Grabber>().notHoldingAnything = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ToxicWasteBin"))
        {
            isTouchingToxicWaste = true;
        }
    }
}
