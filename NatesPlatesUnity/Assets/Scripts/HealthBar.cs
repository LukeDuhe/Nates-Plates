using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float healthLossPerSecond = 2;

    private float maxHealth = 100;
    private float hp = 100;
    private float minHealth = 0;

    private Vector3 initialScale;
    private Vector3 initialPosition;


    void Start()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;

        StartCoroutine("ConstantlyLoseHealth");
    }

    void Update()
    {
        transform.localScale = new Vector3(initialScale.x * (hp / maxHealth), initialScale.y, initialScale.z);
        transform.position = new Vector3(initialPosition.x + ((initialScale.x * .5f) * ((maxHealth - hp) / maxHealth)), initialPosition.y, initialPosition.z);
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
        if ((hp + points) > maxHealth)
        {
            hp = maxHealth;
        }
        else
        {
            hp += points;
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
