using System.Collections;
using System.Collections.Generic;
using FloorInfo;
using UnityEngine;

public class FloorHandler : MonoBehaviour
{
    public int level;
    public FloorType type;


    public void UpdateStars(){
        LevelUpHandler luh = transform.GetComponentInChildren<LevelUpHandler>();
        luh.UpdateStars();
    }
}
