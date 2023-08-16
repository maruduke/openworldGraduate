using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Controller;
using UnityEngine;

namespace Unit.State
{
    class UnitRotateState : MonoBehaviour, IUnitState
    {
        UnitController unitController;
        public void Handle(UnitController controller)
        {
            if (controller != null)
            {
                this.unitController = controller;
            }
        }

        public void Update()
        {
            var btn1 = Input.GetMouseButtonDown(0);
            var btn2 = Input.GetMouseButtonDown(1);
            var btn3 = Input.GetMouseButtonDown(2);

            Debug.Log($"Btn1: {btn1}, Btn2: {btn2}, Btn3: {btn3}");
        }
    }
}
