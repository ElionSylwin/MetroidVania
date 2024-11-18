using UnityEngine;

namespace Vania.Runtime.Player
{
    [CreateAssetMenu(fileName = "PlayerJumpConfig", menuName = "Player/Configs/Jump Config")]
    public class PlayerJumpConfig : BasePlayerConfig
    {
        [SerializeField] private float jumpStrength = 5f;

        public float JumpStrength { get { return jumpStrength; } }
    }
}
