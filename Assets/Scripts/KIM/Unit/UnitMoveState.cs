using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Controller;
using UnityEngine;

namespace Unit.State
{
    class UnitMoveState : MonoBehaviour, IUnitState
    {
        UnitController unitController;
        public void Handle(UnitController controller)
        {
            if (controller != null)
            {
                this.unitController = controller;
            }

            this.unitController.gameObject.transform.position += (this.unitController.gameObject.transform.forward * Time.deltaTime);
        }
    }
}
