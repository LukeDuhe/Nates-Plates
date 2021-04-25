using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleFood : MonoBehaviour
{
    public int points = 0;
    public AudioClip munchingSound;
    private Animator nateAnimator;
    private AudioSource nateAudioSource;
    private ScoreTracker scoreTracker;
    private bool isTouchingNate = false;
    private GameMaster gm;


    private void Awake()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingNate)
        {
            scoreTracker.AddPoints(points);
            GetComponentInParent<Grabber>().notHoldingAnything = true;
            nateAudioSource.PlayOneShot(munchingSound);
            nateAnimator.Play("Nate Eating");
            Destroy(gameObject);
            gm.TakeAction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Nate"))
        {
            nateAnimator = collision.gameObject.GetComponent<Animator>();
            nateAudioSource = collision.gameObject.GetComponent<AudioSource>();
            isTouchingNate = true;
        }
    }
}
