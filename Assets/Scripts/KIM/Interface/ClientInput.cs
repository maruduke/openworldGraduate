using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

using Unit.Controller;

public class ClientInput : MonoBehaviour
{
    UnitController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<UnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            print("up arrow key is held down");
            this.controller.Move();
        }

        if (Input.GetKey("down"))
        {
            this.controller.Rotate();
            print("down arrow key is held down");
        }
    }
}
