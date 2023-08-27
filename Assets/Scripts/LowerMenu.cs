using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class LowerMenu : MonoBehaviour
{

    public AnimationCurve loadCurve;
    public float loadTime;
    public float currrentTime;

    bool isMenuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
