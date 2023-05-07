using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChracterController : MonoBehaviour
{
    public FixedJoystick joy;
    public Rigidbody rigidbody;
    public float speed = 10f;
    public float jumpHeight = 3f;
    public float dash = 5f;
    public float rotSpeed = 3f;
    private Vector3 dir = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate() 
    {
        // 1. Input Value
        dir.x = joy.Horizontal;
        dir.z = joy.Vertical;
        dir = dir * speed * Time.deltaTime;
        rigidbody.MovePosition(rigidbody.position + dir);

        
    }
    
}
