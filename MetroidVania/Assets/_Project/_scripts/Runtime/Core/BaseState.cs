using UnityEngine;

namespace Vania.Runtime.Core
{
    public abstract class BaseState
    {
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void FixedUpdateState();
        public abstract void ExitState();
        public abstract void DoChecks();
    }
}
