using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlateGenerator : MonoBehaviour
{
    // private int frameCounter;
    // private int delay; //Temp delay between creating plate objects
    // private int itemLimit = 18;
    private bool graceFlag = false; //Used to make it less noticeable that there's an item limit

    public int BadSpawnRate = 2; //Lower = more bad plates, minimum is 2
    public float PlateSpawnRate = 5.0f;
    public float PlateGracePeriod = 5.0f;

    public GameObject[] goodArms; //Array containing prefab GameObjects for arms with plates with good objects (foods)
    public GameObject[] badArms; //Array containing prefab GameObjects for arms with plates with bad objects (traps, fire etc.)
    public GameObject[] goodPlates; //Array containing prefab GameObjects for plates with good objects (foods)
    public GameObject[] badPlates; //Array containing prefab GameObjects for plates with bad objects (traps, fire etc.)

    private GameObject armPrefab; //Prefab server arm GameObject
    private GameMaster gm;

    // Start is called before the first frame update
    public void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        StartCoroutine("PlateSpawnTimer");
    }

    //Timing code for spawning plates, increases in frequency as the stage increases
    //If the item limit is reached a grace period starts to give the player time to remove items without instantly adding a new one
    private IEnumerator PlateSpawnTimer()
    {
        while (true)
        {
            if(graceFlag && gm.GetNumItems() >= gm.GetItemLimit()) {
                graceFlag = false;
                yield return new WaitForSeconds(PlateGracePeriod);
            }
            else {
                PlateSpawnRate = gm.GetPlateSpawnRate();
                yield return new WaitForSeconds(PlateSpawnRate);
                GeneratePlate();
            }
            Debug.Log("Grace flag = " + graceFlag);
        }
    }

    //Create a new plate entity
    public void GeneratePlate()
    {
        Vector3 armPosition = new Vector3(0,0,0);
        Vector3 platePosition = new Vector3(0,0,0);
        float armRotate = 0.0f;
        int tableQuadrant = Random.Range(0, 4);
        // tableQuadrant = 2;
        switch(tableQuadrant) {
            case 0: //Left Table Quadrant
                armPosition = new Vector3(-20.0f, Random.Range(-14.0f, 14.0f));
                platePosition = new Vector3(Random.Range(-10.0f, -8.0f), Random.Range(-3.5f, 1.7f));
                armRotate = -90.0f;
                break;
            case 1: //Right Table Quadrant
                armPosition = new Vector3(20.0f, Random.Range(-14.0f, 14.0f));
                platePosition = new Vector3(Random.Range(8.0f, 10.0f), Random.Range(-3.5f, 1.7f));
                armRotate = 90.0f;
                break;
            case 2: //Top Table Quadrant
            case 3:
                armPosition = new Vector3(Random.Range(-20.0f, 20.0f), 14.0f);
                platePosition = new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(-1.0f, 1.7f));
                armRotate = -90.0f;
                break;
            
        }

        //Default to tomato in case random plate generation fails
        armPrefab = goodArms[0];

        //Determine which type of plate to generate based on the current stage
        switch(gm.GetStage())
        {
            case 0: //Tomatoes
                armPrefab = goodArms[0];
                break;
            case 1: //Tomatoes, Potatoes
                armPrefab = goodArms[Random.Range(0,goodArms.Length)];
                break;
            case 2: //Tomatoes, Potatoes, Poop
                if(Random.Range(0, BadSpawnRate) == 0)
                    armPrefab = badArms[0];
                else armPrefab = goodArms[Random.Range(0,goodArms.Length)];
                break;
            case 3: //Tomatoes, Potatoes, Poop, Poison
                if(Random.Range(0, BadSpawnRate) == 0)
                    armPrefab = badArms[Random.Range(0, 2)];
                else armPrefab = goodArms[Random.Range(0,goodArms.Length)];
                break;
            case 4: //Tomatoes, Potatoes, Poop, Poison, Toxic Waste
            case 5:
                if(Random.Range(0, BadSpawnRate) == 0)
                    armPrefab = badArms[Random.Range(0, badArms.Length)];
                else armPrefab = goodArms[Random.Range(0,goodArms.Length)];
                break;
        }
        
        //Create Arm GameObject
        GameObject armObject = Instantiate(armPrefab, armPosition, Quaternion.identity) as GameObject;
        ArmMover armScript = armObject.GetComponent(typeof(ArmMover)) as ArmMover;
        armScript.Init(platePosition, armRotate, gameObject);

        //Determine if the item limit has been reached, triggering a grace period
        if(gm.AddItem() > gm.GetItemLimit()) graceFlag = true;
    }
    
}
