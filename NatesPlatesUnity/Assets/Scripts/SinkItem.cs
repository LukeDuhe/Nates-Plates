using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkItem : MonoBehaviour
{
    public AudioClip sinkSound;
    public int points = 0;
    private ScoreTracker scoreTracker;
    private bool isTouchingSink = false;
    private GameMaster gm;
    private GameObject sink;

    private void Awake()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingSink)
        {
            sink.GetComponent<AudioSource>().PlayOneShot(sinkSound);
            scoreTracker.AddPoints(points);
            GetComponentInParent<Grabber>().notHoldingAnything = true;
            gm.TakeAction();
            gm.RemoveItem();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sink"))
        {
            sink = collision.gameObject;
            isTouchingSink = true;
        }
    }
}
