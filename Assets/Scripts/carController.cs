using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    private static float REGULAR_SPEED = 75.0f;
    private static float FAST_SPEED = 100.0f;
    private static float SLOW_SPEED = 50.0f;
    private float speed = REGULAR_SPEED;
    private float sidemov = 0.0f;
    public float gravity = 20.0f;
    private bool left = true;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
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
        speed = targetSpeed;
        yield return new WaitForSeconds(4.0f);
        speed = REGULAR_SPEED;
    }

    void OnTriggerEnter(Collider c)
    {
		if(c.gameObject.CompareTag("booster")){
           
           StopCoroutine(ChangeSpeed(FAST_SPEED));
           StartCoroutine(ChangeSpeed(FAST_SPEED));
           
		}else if(c.gameObject.CompareTag("slower")){
            StopCoroutine(ChangeSpeed(SLOW_SPEED));
           StartCoroutine(ChangeSpeed(SLOW_SPEED));
        }
        Destroy(c.gameObject);
    }
}