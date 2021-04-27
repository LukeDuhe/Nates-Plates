using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicItem : MonoBehaviour
{
    public int points = 0;
    private ScoreTracker scoreTracker;
    private bool isTouchingToxicWaste = false;
    private GameMaster gm;
    private AudioClip nuclearSound;
    private GameObject toxicWasteBin;

    private void Awake()
    {
        nuclearSound = Resources.Load<AudioClip>("OtherSounds/Nuclear");
        scoreTracker = FindObjectOfType<ScoreTracker>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingToxicWaste)
        {
            toxicWasteBin.GetComponent<AudioSource>().PlayOneShot(nuclearSound);
            scoreTracker.AddPoints(points);
            GetComponentInParent<Grabber>().notHoldingAnything = true;
            gm.TakeAction();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ToxicWasteBin"))
        {
            toxicWasteBin = collision.gameObject;
            isTouchingToxicWaste = true;
        }
    }
}
