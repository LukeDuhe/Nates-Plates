using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100;
    public float hp = 100;
    public float minHealth = 0;

    private Vector3 initialScale;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(initialScale.x * (hp / maxHealth), initialScale.y, initialScale.z);
        transform.position = new Vector3(initialPosition.x + ((initialScale.x * .5f) * ((maxHealth - hp) / maxHealth)), initialPosition.y, initialPosition.z);
    }
}
