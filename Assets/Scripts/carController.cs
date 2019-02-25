using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    public float speed = 75.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private bool left = true;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();


        // let the gameObject fall down
       
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

			moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetMouseButtonDown(0))
            {   
                if(left==true){
                    controller.enabled = false;
                    controller.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y,-13);
                    controller.enabled = true;
                    left=false;
                }else{
                    controller.enabled = false;
                    controller.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y,13);
                    controller.enabled = true;
                    left=true;
                }
            }
		} else {
			
			moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
		}

        // Apply gravity
		moveDirection.x = moveDirection.x + (speed * Time.deltaTime);

        // Move the controller
        controller.Move(moveDirection);
    }

    
    void OnTriggerEnter(Collider c)
    {
		if(c.name.Contains("booster")){
           
           speed = 110.0f;
           
		}else if(c.name.Contains("slower")){
           speed = 50.0f;
       }
       Destroy(c.gameObject);
    }
}