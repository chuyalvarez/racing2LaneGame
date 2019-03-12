using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    private static float REGULAR_SPEED = 75.0f;
    private static float FAST_SPEED = 130.0f;
    private static float SLOW_SPEED = 50.0f;
    private float speed = REGULAR_SPEED;
    private float sidemov = 0.0f;
    public float gravity = 20.0f;
    private bool left = true;
    private int goodbuff=0;
    private int badbuff=0;
    private bool coroutinerunning = false;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private IEnumerator fast;
    private IEnumerator slow;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    void Update()
    {   
        Debug.Log(Time.time);
        if(!coroutinerunning){
            if (goodbuff >0){
                goodbuff--;
                fast=ChangeSpeed(FAST_SPEED);
                StartCoroutine(fast);
            } else if(badbuff >0){
                badbuff--;
                slow=ChangeSpeed(SLOW_SPEED);
                StartCoroutine(slow);  
            } else if(badbuff<1 && goodbuff<1){
                speed=REGULAR_SPEED;
            }
        }
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(1.0f, 0.0f, 1.0f);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x = moveDirection.x * speed;
            moveDirection.z = moveDirection.z * sidemov;

            if (Input.GetMouseButtonDown(0))
            {   
                if(left){
                    sidemov=25.0f;
                    left=false;
                }else{
                    sidemov=-25.0f;
                    left=true;
                }
            }
        }
        if((transform.position.z<-12.0&&!left)||(transform.position.z>12.0&&left)){
            sidemov=0.0f;
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
        
    }

    IEnumerator ChangeSpeed(float targetSpeed){
        coroutinerunning=true;
        speed = targetSpeed;
        yield return new WaitForSeconds(2.0f);
        coroutinerunning=false;
    }

    void OnTriggerEnter(Collider c)
    {
		if(c.gameObject.CompareTag("booster")){
            badbuff = 0;
            goodbuff +=1;
            if (coroutinerunning && speed<REGULAR_SPEED){
                StopCoroutine(slow);
                coroutinerunning=false;
            }
            
           
		}else if(c.gameObject.CompareTag("slower")){
            goodbuff = 0;
            badbuff += 1;
            if (coroutinerunning && speed>REGULAR_SPEED){
                StopCoroutine(fast);
                coroutinerunning=false;
            }
           
        }
        Destroy(c.gameObject);
    }
}