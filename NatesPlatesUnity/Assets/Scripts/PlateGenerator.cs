using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlateGenerator : MonoBehaviour
{
    private int frameCounter;
    private int delay; //Temp delay between creating plate objects
    public GameObject[] goodPlates; //Array containing prefab GameObjects for plates with good objects (foods)
    public GameObject[] badPlates; //Array containing prefab GameObjects for plates with bad objects (traps, fire etc.)

    // Start is called before the first frame update
    void Start()
    {
        frameCounter = 0;
        delay = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(frameCounter++ > delay) {
            frameCounter = 0;
            GeneratePlate();
        }
    }

    //Create a new plate entity
    void GeneratePlate()
    {
        var position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
        Instantiate(goodPlates[Random.Range(0, goodPlates.Length-1)], position, Quaternion.identity);

    }
}
