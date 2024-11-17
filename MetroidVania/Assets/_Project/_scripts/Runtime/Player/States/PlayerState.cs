using UnityEngine;
using Vania.Runtime.Core;

namespace Vania.Runtime.Player
{
    public class PlayerState : BaseState
    {
        protected BaseStateMachine stateMachine;

        public override void DoChecks()
        {
            throw new System.NotImplementedException();
        }

        public override void EnterState(BaseStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public override void FixedUpdateState()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnMoveInput(Vector2 input) { }
    }
}
