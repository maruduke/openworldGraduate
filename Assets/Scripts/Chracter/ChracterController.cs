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

    public float speed = 2f;
    public float jumpHeight = 3f;
    public float dash = 5f;
    public float rotSpeed = 10f;

    private Vector3 dir = Vector3.zero;

    


    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        anim.SetBool("Run", false);
        
    }

    // Update is called once per frame
    void Update()
    {

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
            transform.Translate(movement, Space.World);
        }

        else {
            anim.SetBool("Run", false);
        }

    }
    


    public void speedExchange(bool runStatus)
    {
        if(runStatus) speed = 50f;
        else speed = 10f;
    }
}
