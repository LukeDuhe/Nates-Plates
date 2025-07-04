using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    /*
    What each stage introduces
    Stage Level 0: Tomatoes
    Stage Level 1: Potatoes
    Stage Level 2: Poop
    Stage Level 3: Poison
    Stage Level 4: Toxic Waste
    Stage Level 5: Health falls faster
    */
    private int stage; //Stage Level is used to determine what plates to spawn and how often, as well as scoring
    private int actionsTaken; //Stage increments as the player takes more actions (eats, throws plates away, etc.)
    private int numItems;
    

    public float plateSpawnRate = 4.0f; //How often to spawn the arms in seconds
    public const int itemLimit = 18;


    // Start is called before the first frame update
    void Start()
    {
        actionsTaken = 0;
        stage = 0;
        Debug.Log("Plate Rate = " + plateSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        if(actionsTaken > stage*2 && stage < 5) {
            actionsTaken = 0;
            // Debug.Log("Incrementing Stage");
            stage++;
            //Some stages add a game element when they're reached
            switch(stage)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    GameObject.Find("Floating-Trash").GetComponent<RecepticalMovement>().BringItIn();
                    Debug.Log("Enter Garbage from stage left.");
                    plateSpawnRate -= 0.25f;
                    Debug.Log("Plate Rate = " + plateSpawnRate);
                    break;
                case 4:
                    GameObject.Find("GloveSpotLeft").GetComponent<RecepticalMovement>().BringItIn();
                    GameObject.Find("GloveSpotRight").GetComponent<RecepticalMovement>().BringItIn();
                    GameObject.Find("Floating-NukeWaste").GetComponent<RecepticalMovement>().BringItIn();
                    Debug.Log("Enter Toxic Barrel from stage right.");
                    plateSpawnRate -= 0.25f;
                    Debug.Log("Plate Rate = " + plateSpawnRate);
                    break;
                case 5:
                    Debug.Log("Famine Mode Activated!");
                    GameObject.Find("HealthBar").GetComponent<HealthBar>().healthLossPerSecond = 2;
                    plateSpawnRate -= 0.5f;
                    Debug.Log("Plate Rate = " + plateSpawnRate);
                    break;
            }
        }
    }

    public int GetStage() {
        return stage;
    }

    public float GetPlateSpawnRate()
    {
        return plateSpawnRate;
    }

    public int AddItem()
    {
        return ++numItems;
    }

    public int RemoveItem()
    {
        return --numItems;
    }

    public int GetItemLimit()
    {
        return itemLimit;
    }

    public int GetNumItems()
    {
        return numItems;
    }

    public void TakeAction() {
        // Debug.Log("Action Taken");
        actionsTaken++;
    }
}
