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

    private float vInput;
    private float hInput;

    //Adds a private Rigidbody-type variable which contains the capsule's Rigidbody component info
    private Rigidbody _rb;

    //fires when a script is initialized; the player hits PLAY
    void Start()
    {
        //checks if Rigidbody exists on the GameObject the script is attached to
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        /*comment out so we don't run two types of player controls
         
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }
    void FixedUpdate() //frame rate independent
    {
        //Input.Get.KeyDown () returns a bool value
        //the method accepts a key parameter as either a string or a KeyCode
        //we check for KeyCode.Space

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //passing the Vector3 and ForceMode parameters to RigidBody.AddForce() makes the player jump
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        //Vector3 stores left and right rotation
        Vector3 rotation = Vector3.up * hInput; 

        //Quaternion.Euler takes a Vector3 parameter & returns rotation value in Euler angles
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

       //calls MovePosition on our _rb component fr/ Vector3 component and applies force
        _rb.MovePosition(this.transform.position + this.transform.forward
            * vInput * Time.fixedDeltaTime);

        //calles MoveRotate on the _rb component fr/ Vector3 component and applies force "under the hood" ??
        _rb.MoveRotation(_rb.rotation * angleRot);
    }
}
