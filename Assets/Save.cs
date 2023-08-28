using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloorInfo;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class Save : MonoBehaviour
{

    [SerializeField] 
    private SaveFormat saveData;

    [SerializeField] 
    private SaveFormat loadData;

    public bool debugState = true;

    public Transform autoSave;

    public GameObject debugButton;

    BuildingLogic bl;
    MoneyLogic ml;

    void Start()
    {
        bl = GetComponent<BuildingLogic>();
        ml = GetComponent<MoneyLogic>();
        Debug.Log("Savedata can be found here!: " + Application.persistentDataPath + "/SaveData.json");
    
        if (!Application.isEditor || debugState)
        {
            LoadFromJson();
        }

        if(Application.isEditor || Debug.isDebugBuild){
            debugButton.SetActive(true);
        }

        
        autoSave.gameObject.SetActive(false);
        InvokeRepeating("AutoSave", 30f, 60f); // after 30 seconds start, every min
        // AutoSave();
    }

    // Update is called once per frame
    public async void AutoSave(){
        autoSave.gameObject.SetActive(true);
        float autoSaveTime = 3f;
        float timer = 0;
        SaveIntoJson();
        while(timer <= autoSaveTime){
            autoSave.Rotate(0f, 0f, 0.5f, Space.Self);
            timer += Time.fixedDeltaTime;
            await Task.Yield();
        }
        
        autoSave.gameObject.SetActive(false);
    }

    public void ClearJson(){
        System.IO.File.Delete(Application.persistentDataPath + "/SaveData.json");
    }


     public void SaveIntoJson(){
        saveData = new SaveFormat();

        saveData.money = ml.money;

        foreach (FloorHandler fd in bl.FDs)
        {
            SaveFloor sf = new SaveFloor();
            sf.level = fd.level;
            sf.floorType = fd.type;
            saveData.floors.Add(sf);
        }

        string rawSave = JsonUtility.ToJson(saveData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/SaveData.json", rawSave);
    }

    public void LoadFromJson(){
        loadData = new SaveFormat();
        string rawSave;
        try
        {
            rawSave = System.IO.File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
        }
        catch (System.Exception)
        {
            Debug.Log("No save found");
            return;
            throw;
        }
        loadData = JsonUtility.FromJson<SaveFormat>(rawSave);



        // Change data based on save data
        ml.money = loadData.money; // Money

        bl.ClearFloors();

        foreach (SaveFloor currentFloor in loadData.floors)
        {
            if(currentFloor.floorType == FloorType.Bedroom){
                bl.CreateFloor(bl.roomPrefab, currentFloor.level);
            }

            if(currentFloor.floorType == FloorType.Lobby){
                bl.CreateFloor(bl.lobbyPrefab, currentFloor.level);
            }
        }

    }

}
[System.Serializable]
public class SaveFormat{
    public float money;
    public List<SaveFloor> floors = new List<SaveFloor>();
}
[System.Serializable]
public class SaveFloor{
    public int level;
    public FloorType floorType;
}

