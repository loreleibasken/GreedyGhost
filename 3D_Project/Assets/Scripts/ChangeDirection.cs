using UnityEngine;
using System.Collections;

public class ChangeDirection : MonoBehaviour {

  

    void Start ()
    {
       
    }


    void Update()
    {
        
      


    }


    /*
    void OnTriggerEnter(Collider c)
    {
        // force is how forcefully we will push the player away from the enemy.
        float force = 3;

        // If the object we hit is the enemy
        if (c.gameObject.tag == "Vase")
        {
            // Calculate Angle Between the collision point and the player
            Vector3 dir = c.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
           // GetComponent<Rigidbody>().AddForce(dir * force);
        }
    }
    */





   
}

