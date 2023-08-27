using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloorInfo;
using TMPro;

public class MoneyLogic : MonoBehaviour
{

    public float money;
    public TextMeshProUGUI moneyUI;


    BuildingLogic bl;

    // Start is called before the first frame update
    void Start()
    {
        bl = GetComponent<BuildingLogic>();
        InvokeRepeating("Cashflow", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI(){
        moneyUI.text = string.Format("MONEY: {0:C}", money);
    }

    void Cashflow(){
        float income = 0;
        // BOOTY PERFORECE DONT DONT THIS FIIX LATER
        for (int i = 0; i < bl.FDs.Count; i++)
        {
            if(bl.floors[i].floorType == FloorType.Bedroom){
                income += (bl.FDs[i].level/5f) + 1;
            }
        }

        money += income;
        Debug.Log("Money has gone up "+ income+"!");
        moneyUI.text = string.Format("MONEY: {0:C}", money);
        // every floor generate 1 dollar a second;



        // level muliplers
        // lv1 = 1
        // lv2 = 1.2
        // lv3 = 1.4
        // lv / 5 
    }
}
