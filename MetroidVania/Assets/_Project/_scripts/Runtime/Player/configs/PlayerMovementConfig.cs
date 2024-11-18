using UnityEngine;

namespace Vania.Runtime.Player
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Player/Configs/Movement Config")]
    public class PlayerMovementConfig : BasePlayerConfig
    {
        [SerializeField] private float groundDist;
        [SerializeField] private float movementSpeed;

        public float GroundDist {  get { return groundDist; } }
        public float MovementSpeed { get {  return movementSpeed; } }
    }
}
