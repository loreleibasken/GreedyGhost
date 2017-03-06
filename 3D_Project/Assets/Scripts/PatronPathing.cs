using UnityEngine;
using System.Collections;

public class PatronPathing : MonoBehaviour
{

    // public Transform target;
    // NavMeshAgent agent;
    public float moveSpeed;
    public float walkTime;
    public float waitTime;
    public bool isWalking;

    private Rigidbody myRigidbody;
    private float waitCounter;
    private float walkCounter;
    private int walkDirection;

	
	void Start ()
    {

        // agent = GetComponent<NavMeshAgent>();
        myRigidbody = GetComponent<Rigidbody>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        chooseDirection();
	
	}
	
	
	void Update ()
    {

        // agent.SetDestination(target.position);
        if(isWalking)
        {
            walkCounter -= Time.deltaTime;

          

            switch(walkDirection)
            {
                case 0:
                    myRigidbody.velocity = new Vector3(0, moveSpeed);
                    break;

                case 1:
                    myRigidbody.velocity = new Vector3(moveSpeed, 0);
                    break;

                case 2:
                    myRigidbody.velocity = new Vector3(0, -moveSpeed);
                    break;

                case 3:
                    myRigidbody.velocity = new Vector3(-moveSpeed, 0);
                    break;
            }

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }

        }
        else
        {
            waitCounter -= Time.deltaTime;

            myRigidbody.velocity = Vector3.zero;

            if(waitCounter < 0)
            {
                chooseDirection();
            }
        }
	
	}

    public void chooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}
