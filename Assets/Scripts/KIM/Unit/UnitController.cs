using UnityEngine;
using Unit.State;

namespace Unit.Controller
{
    interface IUnitController
    {
        void Rotate();

        void Move();
        void Jump();
        void Attack();
        void Dig();
    }

    class UnitController : MonoBehaviour, IUnitController
    {
        IUnitState moveState;
        IUnitState jumpState;
        IUnitState attackState;
        IUnitState digState;
        UnitStateContext context;

        private float maxMoveSpeed = 1f;
        private float maxJumpSpeed = 1f;

        public float currentSpeed = 1f;

        private void Awake()
        {
        }

        private void Start()
        {
            this.context = new UnitStateContext(this);
            this.moveState = gameObject.AddComponent<UnitMoveState>();
        }

        public void Attack()
        {
        }

        public void Dig()
        {
        }

        public void Jump()
        {
        }

        public void Rotate()
        {
            this.context.Transition(this.moveState);
        }

        public void Move()
        {
            this.context.Transition(this.moveState);
        }
    }
}
