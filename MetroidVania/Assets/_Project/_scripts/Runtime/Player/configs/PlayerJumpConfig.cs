using UnityEngine;

namespace Vania.Runtime.Player
{
    [CreateAssetMenu(fileName = "PlayerJumpConfig", menuName = "Player/Configs/Jump Config")]
    public class PlayerJumpConfig : BasePlayerConfig
    {
        [SerializeField] private float jumpStrength;
        [SerializeField] private float doubleJumpWindow;

        public float JumpStrength { get { return jumpStrength; } }
        public float DoubleJumpWindow { get { return doubleJumpWindow; } }
    }
}
