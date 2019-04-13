using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PathCreation.Examples
{

    public class PowerUpController : MonoBehaviour
    {
        public GameObject booster1;
        public GameObject booster2;
        public GameObject booster3;
        public GameObject booster4;
        public GameObject booster5;
        public GameObject slower;
        public PathCreator pathCreator;
        public PathFollower player;
        private Vector3 pos;
        private Vector3 pos2;
        private Quaternion rot;
        private int roll;
        private int number;
        private int side;
        private GameObject temp;
        public EndOfPathInstruction endOfPathInstruction;
        public Text timeRemaining;
        private float timeToFinish;
        private float timeleft;

        // Start is called before the first frame update
        void Start(){
            timeToFinish = 65;
            for (int i = 0; i < 23; i++){
                pos = pathCreator.path.GetPointAtDistance((300.0f * i) + 500, endOfPathInstruction);
                pos2 = pathCreator.path.GetPointAtDistance((300.0f * i)+510, endOfPathInstruction);
                roll = Random.Range(0, 2);
                number = Random.Range(0, 5);
                side = Random.Range(0, 2)*2-1;

                if (roll == 0){
                    if (number == 0){
                        temp = Instantiate(booster1, new Vector3(pos.x, pos.y + 10, pos.z), new Quaternion());
                    }
                    else if (number == 1){
                        temp = Instantiate(booster2, new Vector3(pos.x, pos.y + 10, pos.z), new Quaternion());
                    }
                    else if (number == 2){
                        temp = Instantiate(booster3, new Vector3(pos.x, pos.y + 10, pos.z), new Quaternion());
                    }
                    else if (number == 3){
                        temp = Instantiate(booster4, new Vector3(pos.x, pos.y + 10, pos.z), new Quaternion());
                    }
                    else if (number == 4){
                        temp = Instantiate(booster5, new Vector3(pos.x, pos.y + 10, pos.z), new Quaternion());
                    }
                }
                else if (roll == 1){
                    temp = Instantiate(slower, new Vector3(pos.x, pos.y + 12, pos.z), new Quaternion());
                }
                
                temp.transform.LookAt(pos2);
                temp.transform.Rotate(-50, 0, 0, Space.Self);
                temp.transform.Rotate(0, 90, 0, Space.Self);
                temp.transform.position = temp.transform.position + (temp.transform.forward * 20 * side);
            }
        }

        // Update is called once per frame
        void Update()
        {
            timeleft = timeToFinish - Time.timeSinceLevelLoad;
            if (timeleft > 0)
            {
                timeRemaining.text = "Tiempo Restante: " + (timeleft).ToString("F2"); 
            }else
            {
                SceneManager.LoadScene("FailedScene");
            }
        }

        public void setFinalTime()
        {
            PlayerPrefs.SetFloat("time", timeToFinish - timeleft);
            SceneManager.LoadScene("EndingScene");
        }
    }
}
