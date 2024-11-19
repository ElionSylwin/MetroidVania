using UnityEngine;

namespace Vania.Runtime.Player
{
    [CreateAssetMenu(fileName = "PlayerDashConfig", menuName = "Player/Configs/Dash Config")]
    public class PlayerDashConfig : BasePlayerConfig
    {
        [SerializeField] private float dashStrength;
        [SerializeField] private float dashLength;

        public float GetDashStrength { get { return dashStrength; } }
        public float GetDashLength { get {  return dashLength; } }
    }
}
