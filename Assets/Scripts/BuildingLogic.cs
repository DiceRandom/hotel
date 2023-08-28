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

    public GameObject StarBorder;
    public GameObject TitleBorder;

    public List<Floor> floors = new List<Floor>();
    
    public List<FloorHandler> FDs = new List<FloorHandler>();

    public bool debug = true;
    

    // Start is called before the first frame update
    void Awake()
    {
        // create lobby

        CreateFloor(lobbyPrefab,0);
        
        CreateFloor(roomPrefab,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            CreateFloor(roomPrefab,0);
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
            CreateFloor(roomPrefab,0);
            Debug.Log("Debug Floor");
            return;
        }   
        if(GetComponent<MoneyLogic>().money >= price){
            GetComponent<MoneyLogic>().money -= price;
            CreateFloor(roomPrefab,0);
        }else{
            Debug.LogError("BROKE AF");
        }
        GetComponent<MoneyLogic>().UpdateUI();
    }



    public void CreateFloor(Floor floorItem, int level){

        // Create
        GameObject newFloor = new GameObject(floorItem.floorType.ToString());
        // Set starting values
        newFloor.transform.position = transform.position;
        newFloor.transform.localScale = new Vector3(4.7f,4.7f,4.7f);
        // Set the texture
        newFloor.AddComponent<SpriteRenderer>().sprite = floorItem.floorTexture;


        // shader
        GameObject shaderItem = new GameObject("shader");
        shaderItem.transform.parent = newFloor.transform;
        shaderItem.transform.position = transform.position;
        shaderItem.transform.localScale = new Vector3(1f,1f,1f);
        shaderItem.AddComponent<SpriteRenderer>().sprite = floorItem.shaderTexture;
        shaderItem.GetComponent<SpriteRenderer>().sortingOrder = 2; // dumb


        // these fd lines are dumb
        FloorHandler tempFD = newFloor.AddComponent<FloorHandler>();
        tempFD.level = level;
        tempFD.type = floorItem.floorType;
        FDs.Add(tempFD);

        //  now all the floor stuff
        newFloor.transform.position += new Vector3(0,floors.Count() * size,0); //  this is so fucking dumb
        floors.Add(floorItem);
        newFloor.transform.SetParent(transform);

        // BORDER / LEVELUP
        GameObject border = null;

        switch (floorItem.floorType)
        {
            case FloorType.Lobby:
                border = Instantiate(TitleBorder,newFloor.transform.position,newFloor.transform.rotation,newFloor.transform);
                break;

            case FloorType.Bedroom:
                border = Instantiate(StarBorder,newFloor.transform.position,newFloor.transform.rotation,newFloor.transform);
                break;

            default:
                Debug.LogError($"Type of {floorItem.floorType} is not set! Go to Building Logic to fix.");
                break;
        }
        border.GetComponent<LevelUpHandler>().floorName.text = floorItem.floorType.ToString();
        
        // tempFD.UpdateStars();
        border.GetComponent<LevelUpHandler>().UpdateStars(); // handle loading
    }

    void UpgradeFloor(int floorNumber){
        FDs[floorNumber].level++;
    }

    public void ClearFloors(){
        // CLEARS ALL FLOORS
        // floors
        // FDs
        // and children
        
        floors.Clear();
        FDs.Clear();
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }


}
