using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using FloorInfo;
public class BuildingLogic : MonoBehaviour
{

    public float size; // of floor

    public Floor lobbyPrefab;
    public Floor roomPrefab;

    public GameObject LevelUpItem;

    public List<Floor> floors = new List<Floor>();
    
    public List<FloorHandler> FDs = new List<FloorHandler>();

    public bool debug = true;
    

    // Start is called before the first frame update
    void Start()
    {
        // create lobby

        CreateFloor(lobbyPrefab);
        
            CreateFloor(roomPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            CreateFloor(roomPrefab);
        }

        // if(Input.GetKeyDown(KeyCode.R)){
        //     UpdateFloors();
        // }

        if(Input.GetKeyDown(KeyCode.U)){
            UpgradeFloor(1);
        }
    }


    public void CreateNewFloor(int price){
        if(debug){
            CreateFloor(roomPrefab);
            Debug.Log("Debug Floor");
            return;
        }   
        if(GetComponent<MoneyLogic>().money >= price){
            GetComponent<MoneyLogic>().money -= price;
            CreateFloor(roomPrefab);
        }else{
            Debug.LogError("BROKE AF");
        }
        GetComponent<MoneyLogic>().UpdateUI();
    }



    void CreateFloor(Floor floorItem){

        // Create
        GameObject newFloor = new GameObject(floorItem.floorType.ToString());
        // Set starting values
        newFloor.transform.position = transform.position;
        newFloor.transform.localScale = new Vector3(4.7f,4.7f,4.7f);
        // Set the texture
        newFloor.AddComponent<SpriteRenderer>().sprite = floorItem.floorTexture;

        // these fd lines are dumb
        FloorHandler tempFD = newFloor.AddComponent<FloorHandler>();
        tempFD.level = floorItem.level;
        FDs.Add(tempFD);


        //  now all the floor stuff
        newFloor.transform.position += new Vector3(0,floors.Count() * size,0); //  this is so fucking dumb
        floors.Add(floorItem);
        newFloor.transform.SetParent(transform);

        if(floorItem.floorType == FloorType.Lobby){return;}
        GameObject lvi = Instantiate(LevelUpItem,newFloor.transform.position,newFloor.transform.rotation,newFloor.transform);
    }

    void UpgradeFloor(int floorNumber){
        FDs[floorNumber].level++;
    }


}
