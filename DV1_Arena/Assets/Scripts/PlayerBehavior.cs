using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;

    //jumpVelocity is a variable that holds the amount of applied jump force we want
    public float jumpVelocity = 5f;

    //checks the distance between the player Capsule Collider and any Ground Layer object
    public float distanceToGround = 0.1f;

    //LayerMask is set in the Inspector and is used for collider detection
    public LayerMask groundLayer;

    //bullet stores the Bullet prefab
    public GameObject bullet;
    public float bulletSpeed = 100f;

    private float vInput;
    private float hInput;

    //Adds a private Rigidbody-type variable which contains the capsule's Rigidbody component info
    private Rigidbody _rb;

    //stores the player's Capsule Collider component
    private CapsuleCollider _col;

    //fires when a script is initialized; the player hits PLAY
    void Start()
    {
        //checks if Rigidbody exists on the GameObject the script is attached to
        _rb = GetComponent<Rigidbody>();

        //GetComponent() finds and returns the CapsuleCollider attached to the player
        _col = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        /*comment out so we don't run two types of player controls
         
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */

        //Input.Get.KeyDown () returns a bool value
        //the method accepts a key parameter as either a string or a KeyCode
        //we check for KeyCode.Space
        //check wherer IsGrounded returns true and the spacebar is pressed before executing the jump code
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {

            //passing the Vector3 and ForceMode parameters to RigidBody.AddForce() makes the player jump
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);

        }
    }
    void FixedUpdate() //frame rate independent
    {   
        //Input.Get.KeyDown () returns a bool value
        //the method accepts a key parameter as either a string or a KeyCode
        //we check for KeyCode.Space

        /*if(IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            //take this out of fixed update
            //passing the Vector3 and ForceMode parameters to RigidBody.AddForce() makes the player jump
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }*/

        //Vector3 stores left and right rotation
        Vector3 rotation = Vector3.up * hInput; 

        //Quaternion.Euler takes a Vector3 parameter & returns rotation value in Euler angles
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

       //calls MovePosition on our _rb component fr/ Vector3 component and applies force
        _rb.MovePosition(this.transform.position + this.transform.forward
            * vInput * Time.fixedDeltaTime);

        //calles MoveRotate on the _rb component fr/ Vector3 component and applies force "under the hood" ??
        _rb.MoveRotation(_rb.rotation * angleRot);

        //checks that Input.GetMouseButtonDown () returns true
        //0 is for the left mouse button
        if(Input.GetMouseButtonDown(0))
        {
            //Instantiate() passes the bullet prefab to assign a GameObject to newBullet
            //use capsule's posiiton to place new bullet in fron of the player to avoid collisions
            //append as GameObject at the end to explicitly cast the returned object to the same type as newBullet
            GameObject newBullet = Instantiate(bullet, this.transform.position +
                new Vector3(1, 0, 0), this.transform.rotation) as GameObject;

            //GetComoponent() returns and stores the Rigidbody component on newBullet
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            //set velocity property of Rigidbody component to player's transform.forwad direction x bulletSpeed
            //velocity keeps our bullets in a straigh-ish line
            bulletRB.velocity = this.transform.forward.normalized * bulletSpeed;
        }
    }

    private bool IsGrounded()
    {
        //local Vector3 stores the position at the bottom of the player's Capsule collider
        //check for any collision with any objects on the Ground layer
        //the bottom of the cillider is the 3D opint at center x, min y, and center z
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
            _col.bounds.min.y, _col.bounds.center.z);

        //local bool stores the result of CheckCapsule () method called from the Physics class
        //arguments:
        //start of the cpasule = middle of Capsule Collider
        //end of the capsule = capsuleBottom
        //radius of the capsule = distanceToGround
        //check collisions on the groundLayer in Inspector
        //ignore triggers = QueryTriggerIneraction.Ignore enum
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom,
            distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

            return grounded;
    }
        
}
