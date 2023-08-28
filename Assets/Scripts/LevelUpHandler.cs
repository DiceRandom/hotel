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

    public void UpdateStars(){
        // I HATE THIS UFKCING CODE

        if(stars[0] == null) return;
        fd = transform.parent.GetComponent<FloorHandler>(); 
        if(fd.level >= 0){
            stars[0].interactable = true;
        }
        if(fd.level >= 1){
            stars[1].interactable = true;
        }
        if(fd.level >= 2){
            stars[2].interactable = true; // max
            transform.GetChild(0).GetComponent<Button>().interactable = false;
        }
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
