using System.Collections;
using UnityEngine;


namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public PowerUpController gcontrol;
        float distanceTravelled;
		private Vector3 pos;

        private static float REGULAR_SPEED = 100.0f;
        private static float FAST_SPEED = 150.0f;
        private static float SLOW_SPEED = 50.0f;
        private float speed = REGULAR_SPEED;
        private bool left = true;
        private int goodbuff = 0;
        private int badbuff = 0;
        private bool coroutinerunning = false;
        private int side=1;

        private CharacterController controller;
        private IEnumerator fast;
        private IEnumerator slow;

        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            if (pathCreator != null)
            {
                if (!coroutinerunning)
                {
                    if (goodbuff > 0)
                    {
                        goodbuff--;
                        fast = ChangeSpeed(FAST_SPEED);
                        StartCoroutine(fast);
                    }
                    else if (badbuff > 0)
                    {
                        badbuff--;
                        slow = ChangeSpeed(SLOW_SPEED);
                        StartCoroutine(slow);
                    }
                    else if (badbuff < 1 && goodbuff < 1)
                    {
                        speed = REGULAR_SPEED;
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (left)
                    {
                        side = -1;
                        left = false;
                    }
                    else
                    {
                        side = 1;
                        left = true;
                    }
                }

                distanceTravelled += speed * Time.deltaTime;
                pos = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                pos = new Vector3(pos.x, 7, pos.z);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
				transform.position = Vector3.MoveTowards(transform.position, pos+(transform.right*20*side), speed);
				
            }
        }

        public float getDistance()
        {
            return this.distanceTravelled;
        }

        IEnumerator ChangeSpeed(float targetSpeed)
        {
            coroutinerunning = true;
            speed = targetSpeed;
            yield return new WaitForSeconds(2.0f);
            coroutinerunning = false;
        }

        void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.CompareTag("booster")){
                badbuff = 0;
                goodbuff += 1;
                if (coroutinerunning && speed < REGULAR_SPEED)
                {
                    StopCoroutine(slow);
                    coroutinerunning = false;
                }
                Destroy(c.gameObject);
            }
            else if (c.gameObject.CompareTag("slower")){
                goodbuff = 0;
                badbuff += 1;
                if (coroutinerunning && speed > REGULAR_SPEED)
                {
                    StopCoroutine(fast);
                    coroutinerunning = false;
                }
                Destroy(c.gameObject);
            }
            else if (c.gameObject.CompareTag("FinishLine"))
            {
                gcontrol.setFinalTime();
            }
            
        }
    }
}