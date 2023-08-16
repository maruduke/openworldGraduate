using System;
using System.Diagnostics;
using Unit.Controller;

namespace Unit.State
{
    interface IUnitState
    {
        void Handle(UnitController controller);
    }

    abstract class UnitState : IUnitState
    {
        public virtual void Handle(UnitController controller) { }
    }

    class UnitStateContext
    {
        IUnitState currentState;
        UnitController controller;
        public UnitStateContext(UnitController controller)
        {
            if (controller != null)
                this.controller = controller;
        }

        public void Transition(IUnitState state)
        {
            this.currentState = state;
            this.currentState.Handle(this.controller);
        }
    }
}