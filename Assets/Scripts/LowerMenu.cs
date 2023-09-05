using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;
using TMPro;

public class LowerMenu : MonoBehaviour
{

    public AnimationCurve loadCurve;
    public float loadTime;
    public float currrentTime;

    public TextMeshProUGUI floorPrice;

    bool isMenuOpen = false;

    BuildingLogic bl;

    // Start is called before the first frame update
    void Start(){
        bl = FindObjectOfType<BuildingLogic>();
        
        UpdateBuyText();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            Toggle();
        }
    } 


    void Toggle() {
        if(isMenuOpen){
            Close();
            isMenuOpen = false;
        }else{
            Load();
            isMenuOpen = true;
        }
    }

    public async void Load(){
        
        UpdateBuyText();
        
        float newY = loadCurve.Evaluate(0);
        currrentTime = 0;

        while(currrentTime < loadTime){
            currrentTime += Time.deltaTime;
            newY = loadCurve.Evaluate(currrentTime/loadTime);

            transform.position = new Vector3(transform.position.x, newY ,transform.position.z);
            await Task.Yield();
        }


    }

    public async void Close(){
        float newY = loadCurve.Evaluate(0);
        currrentTime = loadTime;

        while(currrentTime >  0){
            currrentTime -= Time.deltaTime;
            newY = loadCurve.Evaluate(currrentTime/loadTime);

            transform.position = new Vector3(transform.position.x, newY ,transform.position.z);
            await Task.Yield();
        }
    }

    public void UpdateBuyText(){
        floorPrice.text = ("-" + bl.GetPrice().ToString());
    }

}
