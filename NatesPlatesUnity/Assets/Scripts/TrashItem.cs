using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashItem : MonoBehaviour
{
    public int points = 0;
    private AudioClip trashSound;
    private ScoreTracker scoreTracker;
    private bool isTouchingTrash = false;
    private GameMaster gm;
    private GameObject trashCan;

    private void Awake()
    {
        trashSound = Resources.Load<AudioClip>("OtherSounds/Trash");
        scoreTracker = FindObjectOfType<ScoreTracker>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingTrash)
        {
            trashCan.GetComponent<AudioSource>().PlayOneShot(trashSound);
            scoreTracker.AddPoints(points);
            GetComponentInParent<Grabber>().notHoldingAnything = true;
            gm.TakeAction();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trash"))
        {
            trashCan = collision.gameObject;
            isTouchingTrash = true;
        }
    }
}
