using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChracterController : MonoBehaviour
{
    public FixedJoystick joy;
    public RunController runController;

    public GameObject Cam;
    public Animator anim;
    public GameObject direction;
    private Rigidbody rigidBody;

    public float speed;
    private bool runStatus = false;

    private float jumpHeight = 18f;
    public float dash = 5f;
    public float rotSpeed = 10f;

    private Vector3 dir = Vector3.zero;

    private RaycastHit hit;
    private float raycastDistance = 5f;

    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        anim.SetBool("Run", false);
        
    }

    // Update is called once per frame
    void Update()
    {
        preventThroughTheWall();
    }

    private void FixedUpdate() {
        
        move();      
    }
    


    public void speedExchange(bool isExchangeStatus)
    {
        if(isExchangeStatus)
            runStatus = !runStatus;

        if(runStatus) speed = 30f;
        else speed = 10f;
    }

    public void preventThroughTheWall() {
        
        // Debug.DrawRay(transform.position + (Vector3.up * 0.3f), transform.forward * raycastDistance, Color.red);
        if(Physics.Raycast(transform.position + (Vector3.up * 0.3f), transform.forward, out hit, raycastDistance, ~(1 << 2))) {

            if(hit.distance < 1) {
                speed = speed * Mathf.Floor(hit.distance * 10f) / 10f;
            }
        }
        else {
            speedExchange(false);
        }

    }

/*
    player jump 제어
*/
    public void jump() {

        rigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

/*
    player 움직임 제어
*/

    public void move() {
        Vector3 offset = Cam.transform.forward;
        offset.y = 0;
        offset.Normalize();

        // 1. Input Value
        dir.x = joy.Horizontal;
        dir.z = joy.Vertical;

        

        if(dir != Vector3.zero) {

            anim.SetBool("Run", true);
            Vector3 cameraRelativeInput = Quaternion.LookRotation(offset) * dir;

            if (cameraRelativeInput != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(cameraRelativeInput);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
            }

            Vector3 movement = cameraRelativeInput * speed * Time.deltaTime;
            rigidBody.MovePosition(transform.position + movement);

        }

        else {
            anim.SetBool("Run", false);
        } 

    }
}
