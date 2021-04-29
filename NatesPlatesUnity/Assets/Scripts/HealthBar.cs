using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float healthLossPerSecond = 1;

    private float maxHealth = 100;
    public float hp = 100;
    private float minHealth = 0;

    private Vector3 initialScale;
    private Vector3 initialPosition;

    private Color startingColor;

    private bool ded = false;

    private bool gameOverTriggered = false;

    public GameObject nate;
    public Sprite irateNate;

    public AudioClip roarSound;

    public SpriteRenderer redDangerGlow;

    public AudioSource musicPlayer;

    public TentacleMover leftTentacle;
    public TentacleMover rightTentacle;

    public Text gameOverText;

    void Start()
    {
        startingColor = gameObject.GetComponent<SpriteRenderer>().color;

        initialScale = transform.localScale;
        initialPosition = transform.position;

        StartCoroutine("ConstantlyLoseHealth");
    }

    void Update()
    {
        transform.localScale = new Vector3(initialScale.x * (hp / maxHealth), initialScale.y, initialScale.z);
        transform.position = new Vector3(initialPosition.x + ((initialScale.x * .5f) * ((maxHealth - hp) / maxHealth)), initialPosition.y, initialPosition.z);

        if (hp > 20)
        {
            if (!musicPlayer.isPlaying)
            {
                musicPlayer.Play();
            }

            gameObject.GetComponent<AudioSource>().Stop();
            GetComponent<SpriteRenderer>().color = startingColor;
            redDangerGlow.color = new Color(255, 0, 0, 0);
        }
        if (hp <= 20 && !ded)
        {
            if (musicPlayer.isPlaying)
            {
                musicPlayer.Stop();
            }

            GetComponent<SpriteRenderer>().color = Color.red;
            redDangerGlow.color = new Color(255,0,0,.2f);

            if (!gameObject.GetComponent<AudioSource>().isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
        if (hp == 0)
        {
            ded = true;
        }

        if (!gameOverTriggered && ded)
        {
            gameOverText.enabled = true;
            leftTentacle.enabled = false;
            rightTentacle.enabled = false;
            gameObject.GetComponent<AudioSource>().loop = false;
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().volume = .6f;
            gameObject.GetComponent<AudioSource>().PlayOneShot(roarSound);
            gameOverTriggered = true;
            nate.GetComponent<Animator>().enabled = false;
            nate.GetComponent<SpriteRenderer>().sprite = irateNate;
        }
    }

    private IEnumerator ConstantlyLoseHealth()
    {
        while (true)
        {
            DecreaseHealth(healthLossPerSecond);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void IncreaseHealth(float points)
    {
        if (!ded)
        {
            if ((hp + points) > maxHealth)
            {
                hp = maxHealth;
            }
            else
            {
                hp += points;
            }
        }
    }

    public void DecreaseHealth(float points)
    {
        if ((hp - points) < minHealth)
        {
            hp = minHealth;
        }
        else
        {
            hp -= points;
        }
    }
}
