using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlateGenerator : MonoBehaviour
{
    private int frameCounter;
    private int delay; //Temp delay between creating plate objects
    public GameObject[] goodArms; //Array containing prefab GameObjects for arms with plates with good objects (foods)
    public GameObject[] badArms; //Array containing prefab GameObjects for arms with plates with bad objects (traps, fire etc.)
    public GameObject[] goodPlates; //Array containing prefab GameObjects for plates with good objects (foods)
    public GameObject[] badPlates; //Array containing prefab GameObjects for plates with bad objects (traps, fire etc.)
    private GameObject armPrefab; //Prefab server arm GameObject

    // Start is called before the first frame update
    public void Start()
    {
        frameCounter = 0;
        delay = 200;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if(frameCounter++ > delay) {
            frameCounter = 0;
            if(delay > 50) delay-=10;
            GeneratePlate();
        }
    }

    //Create a new plate entity
    public void GeneratePlate()
    {
        Vector3 armPosition = new Vector3(0,0,0);
        Vector3 platePosition = new Vector3(0,0,0);
        float armRotate = 0.0f;
        int tableQuadrant = Random.Range(0, 3);
        // tableQuadrant = 2;
        switch(tableQuadrant) {
            case 0: //Left Table Quadrant
                armPosition = new Vector3(-20.0f, Random.Range(-14.0f, 14.0f));
                platePosition = new Vector3(Random.Range(-10.0f, -8.0f), Random.Range(-6.5f, 1.7f));
                armRotate = -90.0f;
                break;
            case 1: //Top Table Quadrant
                armPosition = new Vector3(Random.Range(-20.0f, 20.0f), 14.0f);
                platePosition = new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(-1.0f, 1.7f));
                armRotate = -90.0f;
                break;
            case 2: //Right Table Quadrant
                armPosition = new Vector3(20.0f, Random.Range(-14.0f, 14.0f));
                platePosition = new Vector3(Random.Range(8.0f, 10.0f), Random.Range(-6.5f, 1.7f));
                armRotate = 90.0f;
                break;
        }
        // var armDirection = armPosition - platePosition
        // Moon.transform.LookAt(Sun.transform);
        //Add Transform.LookAt to the arm

        //  armPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
        //  platePosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);

        //Randomly generate a good plate
        // if(Random.Range(0,2) == 0) armPrefab = badArms[Random.Range(0,badArms.Length)] as GameObject;
        if(Random.Range(0,2) == 0) armPrefab = badArms[2] as GameObject;
        else armPrefab = goodArms[Random.Range(0,goodArms.Length)] as GameObject;
        //Create Arm GameObject
        GameObject armObject = Instantiate(armPrefab, armPosition, Quaternion.identity) as GameObject;
        ArmMover armScript = armObject.GetComponent(typeof(ArmMover)) as ArmMover;
        armScript.Init(platePosition, armRotate, gameObject);
    }
    
}
