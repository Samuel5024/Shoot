using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    //unity automatically calls OnCollision when another object (isTrigger turned off) runs into the item prefab
    void OnCollisionEnter(Collision collision)
    {
        //the property, gameObject, holds reference to the colliding gameObject's Collider
        if (collision.gameObject.name == "Player")
        {
            //destroys the entire Item prefab when collided with by the Player
            Destroy(this.transform.parent.gameObject);

            //prints that we collected the item
            Debug.Log("Item Collected!");
        }
    }
}
