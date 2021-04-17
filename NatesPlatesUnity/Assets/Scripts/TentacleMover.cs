using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleMover : MonoBehaviour
{
    public string horizontal_axis;
    public string vertical_axis;

    public float speed = 8;

    private float h;
    private float v;

    public void Update()
    {
        h = Input.GetAxisRaw(horizontal_axis);
        v = Input.GetAxisRaw(vertical_axis);
    }

    public void FixedUpdate()
    {
        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;

        transform.position += tempVect;
    }
}
