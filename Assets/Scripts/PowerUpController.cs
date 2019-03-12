using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public GameObject booster;
    public GameObject slower;
    private float roll;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 35;i++){
            roll=Random.Range(0,6);
            distance=Random.Range(-50,50);
            if (roll==0){
                Instantiate(booster,new Vector3((250.0f*i)+200.0f-distance,6.5f,10.0f),Quaternion.identity);
            } else if (roll==1){
                Instantiate(slower,new Vector3((250.0f*i)+200.0f-distance,11f,-12.0f),Quaternion.identity);
            } else if (roll==2){
                Instantiate(booster,new Vector3((250.0f*i)+200.0f-distance,6.5f,-10.0f),Quaternion.identity);
            } else if (roll==3){
                Instantiate(slower,new Vector3((250.0f*i)+200.0f-distance,11f,12.0f),Quaternion.identity);
            }else if (roll==4){
                Instantiate(booster,new Vector3((250.0f*i)+200.0f-distance,6.5f,10.0f),Quaternion.identity);
            } else if (roll==5){
                Instantiate(booster,new Vector3((250.0f*i)+200.0f-distance,6.5f,-10.0f),Quaternion.identity);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
