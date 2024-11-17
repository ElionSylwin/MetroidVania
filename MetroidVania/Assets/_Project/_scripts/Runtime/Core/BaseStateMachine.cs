using UnityEngine;

namespace Vania.Runtime.Core
{
    public abstract class BaseStateMachine : MonoBehaviour
    {
        public abstract void Update();
        public abstract void FixedUpdate();
    }
}
