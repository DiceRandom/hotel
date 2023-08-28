using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LevelUpHandler : MonoBehaviour
{
    FloorHandler fd;
    // public TextMeshProUGUI levelText;

    public Selectable[] stars;
    public TextMeshProUGUI floorName;

    void Start()
    {
        fd = transform.parent.GetComponent<FloorHandler>(); 
        // levelText.text = "Level "+(fd.level+1);
    }

    public void LevelUp(){
        if(fd.level == 2){return;}
        fd.level++;
        stars[fd.level].interactable = true;
        // levelText.text = "Level "+(fd.level+1);
        if(fd.level == 2){
            transform.GetChild(0).GetComponent<Button>().interactable = false;
        }
    }
}
